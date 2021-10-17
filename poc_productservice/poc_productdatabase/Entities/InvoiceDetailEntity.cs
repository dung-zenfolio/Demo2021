using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Entities
{
    public class InvoiceDetailEntity : BaseEntity
    {
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double TotalAmount { get; set; }
        public Guid InvoiceId { get; set; }
        public InvoiceEntity Invoice { get; set; }
        public ProductEntity Product { get; set; }
    }
}
