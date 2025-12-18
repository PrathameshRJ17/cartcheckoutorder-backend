namespace FoodOrderingSystem.API.DTOs.Address
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public int? UserId { get; set; }
        public string AddressLine { get; set; } = string.Empty;
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public AddressType Type { get; set; }
        public bool IsDefault { get; set; }
        public string? ContactNumber { get; set; }
        public string? AlternateContactNumber { get; set; }
    }

    public enum AddressType
    {
        Home,
        Work,
        Other
    }
}
