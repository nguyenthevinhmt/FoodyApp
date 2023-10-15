using Foody.Share.Shared.FilterDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class OrderFilterDto : FilterDto
    {
        private string _keyword;
        [FromQuery(Name = "keyword")]
        public string Keyword {  get { return _keyword; } set { _keyword = value.Trim(); } }
        [FromQuery(Name = "createDate")]
        public DateTime? CreateDate {  get; set; }
        [FromQuery(Name = "orderStatus")]
        public int? orderStatus {  get; set; }
    }
}
