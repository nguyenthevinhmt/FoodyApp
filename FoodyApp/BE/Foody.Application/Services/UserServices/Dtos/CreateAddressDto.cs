using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.UserServices.Dtos
{
    public class CreateAddressDto
    {
        public int UserId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string DetailAddress { get; set; }
        public string Notes { get; set; }
        public int AddressType { get; set; }
    }
}
