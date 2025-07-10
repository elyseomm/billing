using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Core.Models
{
    [Table("InvoiceLines")]
    public class InvoiceLine
    {
        [Key]
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal SubTotal { get; set; }
    }
}
