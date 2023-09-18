﻿using System.ComponentModel.DataAnnotations;

namespace Foody.Domain.Entities
{
    public class ProductCart
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
