using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Core.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Active { get; set; }
    }
}
