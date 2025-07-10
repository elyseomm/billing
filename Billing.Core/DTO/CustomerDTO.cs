using System;

namespace Billing.Core.DTO
{
    public class CustomerDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? Active { get; set; }
    }
}
