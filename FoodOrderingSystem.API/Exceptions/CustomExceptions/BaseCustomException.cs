using System.Net;

namespace FoodOrderingSystem.API.Exceptions.CustomExceptions
{
    public abstract class BaseCustomException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }
        public abstract string ErrorCode { get; }

        protected BaseCustomException(string message) : base(message) { }
        protected BaseCustomException(string message, Exception innerException) : base(message, innerException) { }
    }
}