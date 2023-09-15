using System.ComponentModel.DataAnnotations;

namespace Foody.Application.Services.CategoryServices.Dtos
{
    public class CreateCategoryDto
    {
        [StringLength(250)]
        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImageUrl { get; set; } = string.Empty;
    }
}
