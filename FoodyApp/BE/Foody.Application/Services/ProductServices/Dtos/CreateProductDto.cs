using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Foody.Application.Services.ProductServices.Dtos
{
    public class CreateProductDto
    {
        [StringLength(250)]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, double.MaxValue)]
        public double ActualPrice { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [DefaultValue(1)]
        public int PromotionId { get; set; }
        public bool IsActive { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
