using Foody.Application.Shared.FilterDto;

namespace Foody.Application.Services.ProductServices.Dtos
{
    public class ProductFilterDto : FilterDto
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value.Trim(); } }

        public string CategoryId { get; set; }
        public decimal StartPrice { get; set; } = decimal.Zero;
        public decimal EndPrice { get; set; } = decimal.MaxValue;

    }
}
