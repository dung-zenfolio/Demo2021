using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Entities
{
    public class InvoiceEntity : BaseEntity
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Guid UserId { get; set; }
        public ICollection<InvoiceDetailEntity> InvoiceDetails { get; set; }
    }
}
