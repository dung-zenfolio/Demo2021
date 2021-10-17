using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Entities
{
    public class ProductTypeEntity : BaseEntity
    {
        public string TypeName { get; set; }
        public int Rank { get; set; }
        public  ICollection<ProductEntity> Products { get; set; }
    }
}
