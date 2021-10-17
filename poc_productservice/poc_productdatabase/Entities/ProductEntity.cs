using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Guid ProductTypeId { get; set; }
        public double Price { get; set; }
        public string ProductImageUrl { get; set; }
        public int ProductQuantity { get; set; }
        public ProductTypeEntity ProductType { get; set; }
        public ICollection<ProductImagesEntity> Images { get; set; }
        public ICollection<InvoiceDetailEntity> InvoiceDetails { get; set; }
    }
}
