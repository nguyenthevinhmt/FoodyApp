using Foody.Share.Shared.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.ProductServices.Dtos
{
    public class ProductFilterByCategoryDto : FilterDto
    {
        public int CategoryId { get; set; }
    }
}
