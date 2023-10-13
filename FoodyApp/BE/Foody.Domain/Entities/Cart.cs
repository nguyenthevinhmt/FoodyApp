using Foody.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Domain.Entities
{
    public class Cart : BaseEntity<int>, ICreated, ISoftDeleted
    {
        public int UserId { get; set; }
        public IEnumerable<ProductCart> ProductCarts { get; set; }
        public IEnumerable<Product> Products { get; set; }
        #region Audit
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
