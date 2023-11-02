using Foody.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Domain.Entities
{
    public class ProductCart : BaseEntity<int>
    {
        public int Quantity {  get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public bool IsDeleted { get; set; }
    }
}
