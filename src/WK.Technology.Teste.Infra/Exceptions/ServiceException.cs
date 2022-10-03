using System.Globalization;
using System.Runtime.Serialization;

namespace WK.Technology.Teste.Infra.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException() : base() { }

        public ServiceException(string message) : base(string.Format(CultureInfo.CurrentCulture, CustomMessageService(message))) { }

        public ServiceException(string message, Exception inner) : base(string.Format(CultureInfo.CurrentCulture, CustomMessageService(message)), inner) { }

        public ServiceException(SerializationInfo info, StreamingContext context) { }

        public ServiceException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, CustomMessageService(message), args))
        {
        }

        private static string CustomMessageService(string message)
        {
            return $"(Service Exception) - {message}";
        }
    }
}
