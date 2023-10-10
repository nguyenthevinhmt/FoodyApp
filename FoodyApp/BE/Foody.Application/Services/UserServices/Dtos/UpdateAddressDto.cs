using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.UserServices.Dtos
{
    public class UpdateAddressDto : CreateAddressDto
    {
        public int Id { get; set; }
    }
}
