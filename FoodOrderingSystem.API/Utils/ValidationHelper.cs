using FoodOrderingSystem.API.Exceptions.CustomExceptions;

namespace FoodOrderingSystem.API.Utils
{
    public static class ValidationHelper
    {
        public static void ValidatePositiveInteger(int value, string parameterName)
        {
            if (value <= 0)
                throw new BadRequestException($"{parameterName} must be a positive integer");
        }

        public static void ValidateNotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new BadRequestException($"{parameterName} cannot be null");
        }

        public static void ValidateNotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BadRequestException($"{parameterName} cannot be empty");
        }

        public static void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new BadRequestException("Quantity must be greater than 0");
        }
    }
}