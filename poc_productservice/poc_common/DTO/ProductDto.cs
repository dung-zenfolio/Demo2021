using System;
using System.Collections.Generic;
using System.Text;

namespace poc_common.DTO
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Guid ProductTypeId { get; set; }
        public double Price { get; set; }
        public string ProductImageUrl { get; set; }
        public int ProductQuantity { get; set; }
    }
}
