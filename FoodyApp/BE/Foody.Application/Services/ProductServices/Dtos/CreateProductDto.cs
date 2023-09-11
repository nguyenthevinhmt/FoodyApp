using System.ComponentModel.DataAnnotations;

namespace Foody.Application.Services.ProductServices.Dtos
{
    public class CreateProductDto
    {
        [StringLength(250)]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue)]
        public decimal ActualPrice { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Description { get; set; }
        public string ProductImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
