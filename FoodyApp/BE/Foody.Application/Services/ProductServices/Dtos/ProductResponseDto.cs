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
        public double Price { get; set; }
        /// <summary>
        /// Giá bán ra sản phẩm
        /// </summary>
        public double ActualPrice { get; set; }
        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Đường link ảnh sản phẩm
        /// </summary>
        public string ProductImageUrl { get; set; }
        /// <summary>
        /// ID khuyến mãi
        /// </summary>
        //public int PromotionId { get; set; }
        /// <summary>
        /// Tên khuyến mãi
        /// </summary>
        public PromotionResponseDto Promotion { get; set; }
        /// <summary>
        /// Id danh mục sản phẩm
        /// </summary>
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        /// <summary>
        /// Tên danh mục sản phẩm
        /// </summary>
        public int CreateBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
