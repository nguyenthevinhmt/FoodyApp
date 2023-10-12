using Foody.Share.Shared.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.UserServices.Dtos
{
    public class AddressFilterDto : FilterDto
    {
        public int UserId { get; set; }
    }
}
