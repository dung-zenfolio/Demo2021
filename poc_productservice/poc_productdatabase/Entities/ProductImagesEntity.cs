using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Entities
{
    public class ProductImagesEntity : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public int Rank { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
