using Microsoft.AspNetCore.Mvc;

namespace Foody.Share.Shared.FilterDto
{
    public class FilterDto
    {
        [FromQuery(Name = "PageSize")]
        public int PageSize { get; set; }
        [FromQuery(Name = "PageIndex")]
        public int PageIndex { get; set; }
    }
}
