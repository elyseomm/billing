using System;

namespace Billing.Core.DTO
{
    public class invoiceLineDTO
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public string ProductId { get; set; }
        public int? Quantity { get; set; }
        public Decimal? UnitPrice { get; set; }
        public Decimal? SubTotal { get; set; }
    }
}
