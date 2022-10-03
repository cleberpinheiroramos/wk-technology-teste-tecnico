using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WK.Technology.Teste.Domain.Interfaces.Services;

namespace WK.Technology.Teste.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAspNetUserService user)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, user);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IAspNetUserService user)
        {
            var log = new LogModel
            {
                Source = "WK.Technology.Teste.Api",
                Module = "WK.Technology.Teste Web App",
                Event = GetEvent(exception),
                UserName = user.GetUserId().ToString(),
                FullName = user.GetUserId().ToString(),
                IP = context.Connection.RemoteIpAddress?.ToString(),
                Data = string.Empty,
                Date = DateTime.Now
            };

            if (exception is InvalidOperationException)
            {
                log.Level = Enum.GetName(typeof(LogLevel), LogLevel.Warning);
                log.Description = exception.Message;
            }
            else
            {
                log.Level = Enum.GetName(typeof(LogLevel), LogLevel.Error);
                log.Description = exception.Message;
                log.InnerException = exception.InnerException != null ? exception.InnerException.ToString() : string.Empty;
                log.StackTrace = !string.IsNullOrEmpty(exception.StackTrace) ? exception.StackTrace : string.Empty;
            }

            var _serializedLog = JsonConvert.SerializeObject(log);

            if (log.Level == Enum.GetName(typeof(LogLevel), LogLevel.Warning))
                _logger.LogWarning(_serializedLog);
            else
                _logger.LogError(_serializedLog);

            int statusCode = (int)HttpStatusCode.BadRequest;
            string msg = exception?.Message;

            if (exception is UnauthorizedAccessException) { statusCode = (int)HttpStatusCode.Unauthorized; msg = "Tentativa de acesso não autorizado a este item, verifique seu acesso."; }
            if (exception is FormatException) { statusCode = (int)HttpStatusCode.UnsupportedMediaType; msg = "Formato de dados invalido."; }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                success = false,
                data = msg
            }));
        }

        private string GetEvent(Exception ex)
        {
            var _event = string.Empty;
            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                var trace = new StackTrace(ex, true);
                StackFrame stackFrame = trace.GetFrame(0);
                _event = stackFrame.GetMethod().Name;
            }
            return _event;
        }
    }

    public class LogModel
    {
        public LogModel()
        {
            Tracking = Guid.NewGuid();
        }
        /// <summary>
        /// Globally unique identifier of the action to track
        /// </summary>
        public Guid Tracking { get; set; }
        /// <summary>
        /// System of origin of the action. 
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Identification of the source system module. 
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// Criticality of the event (Audit, Information, Error, Warning). 
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// Action or event identifier. 
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// User identifier that generated performed the action
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Name of the user who performed the action 
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Related information to action
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Inner Exception of error
        /// </summary>
        public string InnerException { get; set; }
        /// <summary>
        /// Stack Trace of error
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// Complementary metadata to the action. It can contain requisition data. 
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// IP source origin of action
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Date the action occurred. 
        /// </summary>
        public DateTime Date { get; set; }

    }
}
