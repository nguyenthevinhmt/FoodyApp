using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.UserServices.Dtos
{
    public class AddressResponseDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Tỉnh / thành phố
        /// </summary>
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
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdateBy { get; set; }
    }
}
