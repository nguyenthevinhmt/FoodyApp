namespace Foody.Application.Services.ProductServices.Dtos
{
    public class ProductResponseDto
    {
        /// <summary>
        /// Id sản phẩm
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Giá nhập vào sản phẩm
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Giá bán ra sản phẩm
        /// </summary>
        public decimal ActualPrice { get; set; }
        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ID chương trình khuyến mãi sản phẩm
        /// </summary>
        public int PromotionId { get; set; }
        /// <summary>
        /// Tên chương trình khuyến mại
        /// </summary>
        public string PromotionName { get; set; }
        /// <summary>
        /// Đường link ảnh sản phẩm
        /// </summary>
        public string ProductImageUrl { get; set; }
        /// <summary>
        /// Danh mục của sản phẩm
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Tên danh mục sản phẩm
        /// </summary>
        public string CategoryName { get; set; }
    }
}
