using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WK.Technology.Teste.Infra.Config;
using WK.Technology.Teste.Infra.Results;

namespace WK.Technology.Teste.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        protected readonly AppSettings _appSettings;

        public ApiControllerBase()
        {
        }

        public ApiControllerBase(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        protected IActionResult SuccessDataResult<TClass>(TClass result, string message = null) where TClass : class
        {
            return Ok(new SuccessDataResult<TClass>(result, message));
        }

        protected IActionResult SuccessResult(string result)
        {
            return Ok(new SuccessResult(result));
        }

        protected IActionResult ErrorDataResult<TClass>(TClass result, string message = null) where TClass : class
        {
            return BadRequest(new ErrorDataResult<TClass>(result, message));
        }

        protected IActionResult ErrorResult(string result)
        {
            return BadRequest(new ErrorResult(result));
        }

        protected IActionResult InternalServerError<TClass>(IDataResult<TClass> obj) where TClass : class
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string conteudoJson;
            try
            {
                conteudoJson = JsonConvert.SerializeObject(obj, Formatting.None, settings);
            }
            catch
            {
                conteudoJson = obj.Message ?? "Falha na serialização do erro";
            }

            return new ContentResult()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ContentType = "",
                Content = conteudoJson
            };
        }

        protected IActionResult InternalServerError(IDataResult<Exception> obj)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string conteudoJson;
            try
            {
                conteudoJson = JsonConvert.SerializeObject(obj, Formatting.None, settings);
            }
            catch
            {
                conteudoJson = obj.Message ?? "Falha na serialização do erro";
            }

            return new ContentResult()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ContentType = "",
                Content = conteudoJson
            };
        }

        protected IActionResult InternalServerError(Exception obj, string message = null)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string conteudoJson;
            try
            {
                conteudoJson = JsonConvert.SerializeObject(new ErrorDataResult<Exception>(obj, message), Formatting.None, settings);
            }
            catch
            {
                conteudoJson = obj.Message ?? "Falha na serialização do erro";
            }

            return new ContentResult()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ContentType = "",
                Content = conteudoJson
            };
        }

        protected IActionResult NotFoundResult(string result)
        {
            return NotFound(new ErrorResult(result));

        }

        protected string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        protected void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        public static class Role
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }
    }
}
