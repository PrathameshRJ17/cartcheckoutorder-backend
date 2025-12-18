using System.Net;

namespace FoodOrderingSystem.API.Exceptions.CustomExceptions
{
    public class NotFoundException : BaseCustomException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string ErrorCode => "NOT_FOUND";

        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string entity, object id) : base($"{entity} with ID {id} was not found.") { }
    }
}