using System.Net;

namespace FoodOrderingSystem.API.Exceptions.CustomExceptions
{
    public class BadRequestException : BaseCustomException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public override string ErrorCode => "BAD_REQUEST";

        public BadRequestException(string message) : base(message) { }
    }
}