namespace Foody.Application.Services.OrderServices.Dtos
{
    public class UserAddressDto
    {
        public string Province { get; set; }
        public int AddressType { get; set; }
        public string District { get; set; }
        public string DetailAddress { get; set; }
        public string StreetAddress { get; set; }
        public string Notes { get; set; }
        public string Ward { get; set; }
    }
}
