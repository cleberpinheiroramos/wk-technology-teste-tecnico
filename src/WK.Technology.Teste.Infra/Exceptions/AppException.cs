using System.Globalization;
using System.Net;

namespace WK.Technology.Teste.Infra.Exceptions
{
    [Serializable]
    public class AppException : Exception
    {
        private readonly HttpStatusCode statusCode;

        public AppException(HttpStatusCode statusCode, string message, Exception ex)
            : base(string.Format(CultureInfo.CurrentCulture, message), ex)
        {
            this.statusCode = statusCode;
        }

        public AppException(HttpStatusCode statusCode, string message)
            : base(string.Format(CultureInfo.CurrentCulture, message))
        {
            this.statusCode = statusCode;
        }

        public AppException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public HttpStatusCode StatusCode
        {
            get { return this.statusCode; }
        }
    }
}
