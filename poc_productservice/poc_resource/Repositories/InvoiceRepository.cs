using Microsoft.EntityFrameworkCore;
using poc_productdatabase.Context;
using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_resource.Repositories
{
    public class InvoiceRepository : Repository<InvoiceEntity, ProductContext>
    {
        private ProductContext _context;
        public InvoiceRepository(ProductContext context) : base(context)
        {
            _context = context;
        }

        public async Task<InvoiceEntity> GetInvoiceInfoByInvoiceNo(string invoiceNo)
        {
            return await _context.Invoice
                .Include(x => x.InvoiceDetails)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.InvoiceNo == invoiceNo);
        }
    }
}
