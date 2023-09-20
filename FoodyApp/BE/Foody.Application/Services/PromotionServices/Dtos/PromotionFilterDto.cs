using Foody.Share.Shared.FilterDto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Foody.Application.Services.PromotionServices.Dtos
{
    public class PromotionFilterDto : FilterDto
    {
        private string _keyword { get; set; }
        public string Keyword { get { return _keyword; } set { _keyword = value.Trim(); } }
        [FromQuery(Name = "startTime")]
        [DefaultValue("0001-01-01T00:00:00")]
        public DateTime StartTime { get; set; }
        [FromQuery(Name = "endTime")]
        [DefaultValue("9999-12-31T23:59:59")]
        public DateTime EndTime { get; set; }
    }
}
