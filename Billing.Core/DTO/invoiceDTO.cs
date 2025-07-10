using System;

namespace Billing.Core.DTO
{
    public class InvoiceDTO
    {
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateOnly? Date { get; set; }
        public DateOnly? DueDate { get; set; }
        public long? InvoiceDate { get; set; }     // Ephoctime value
        public decimal? InvoiceAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string BillingLines { get; set; }
        public string CurrencyCode { get; set; }
        public string Currency { get; set; }
        public int? Active { get; set; }
    }
}
