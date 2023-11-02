using Foody.Share.Shared.FilterDto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Foody.Application.Services.ProductServices.Dtos
{
    public class ProductFilterDto : FilterDto
    {
        private string _name;
        [FromQuery(Name = "Name")]
        public string Name { get { return _name; } set { _name = value?.Trim(); } }

        public string CategoryId { get; set; }
        [FromQuery(Name = "startPrice")]
        [DefaultValue(0)]
        public double StartPrice { get; set; }
        [FromQuery(Name = "endPrice")]
        [DefaultValue(999999999)]
        public double EndPrice { get; set; } = double.MaxValue;

    }
}
