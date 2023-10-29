using System.ComponentModel.DataAnnotations;

namespace Foody.Domain.Entities
{
    public class OrderTemp
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Refer sang bảng giỏ hàng
        /// </summary>
        public int CartReferId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public int PaymentMethod { get; set; }
        public string Province { get; set; }
        /// <summary>
        /// Quận / huyện
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// Phường / Xã
        /// </summary>
        public string Ward { get; set; }
        /// <summary>
        /// Tên đường, làng, tổ, số nhà,...
        /// </summary>
        public string StreetAddress { get; set; }
        /// <summary>
        /// Địa chỉ chi tiết
        /// </summary>
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        [StringLength(250)]
        public string DetailAddress { get; set; }
        /// <summary>
        /// Mô tả thêm
        /// </summary>
        [StringLength(250)]
        public string Notes { get; set; }
        /// <summary>
        /// Loại địa chỉ giao hàng (nhà riêng, công ty, khác)
        /// </summary>
        public int AddressType { get; set; }
        public List<OrderDetailTemp> OrderDetailTemps { get; set; }
    }
}
