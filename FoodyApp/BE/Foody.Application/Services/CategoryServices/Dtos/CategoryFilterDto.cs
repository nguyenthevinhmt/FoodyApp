using Foody.Share.Shared.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.CategoryServices.Dtos
{
    public class CategoryFilterDto : FilterDto
    {
        private string _name;
        public string Name 
        { 
            get { return _name; } 
            set { _name = value.Trim(); }
        }
    }
}
