namespace Foody.Application.Services.CategoryServices.Dtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImageUrl { get; set; } = string.Empty;
    }
}
