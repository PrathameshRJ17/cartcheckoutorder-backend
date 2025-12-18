using System.Net;

namespace FoodOrderingSystem.API.Exceptions.CustomExceptions
{
    public class ValidationException : BaseCustomException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public override string ErrorCode => "VALIDATION_ERROR";
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(string message) : base(message) 
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors) : base("Validation failed")
        {
            Errors = errors;
        }
    }
}