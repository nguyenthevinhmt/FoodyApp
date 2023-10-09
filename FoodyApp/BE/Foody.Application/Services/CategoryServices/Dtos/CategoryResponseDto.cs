using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.CategoryServices.Dtos
{
    public class CategoryResponseDto
    {
        /// <summary>
        /// Id category
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tên category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Mô tả category
        /// </summary>
        public string Description { get; set; }
    }
}
