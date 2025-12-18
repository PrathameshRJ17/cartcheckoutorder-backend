using System.Net;

namespace FoodOrderingSystem.API.Exceptions.CustomExceptions
{
    public class UnauthorizedException : BaseCustomException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
        public override string ErrorCode => "UNAUTHORIZED";

        public UnauthorizedException(string message = "User not authenticated") : base(message) { }
    }
}