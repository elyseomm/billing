using System.Text.Json.Serialization;

namespace BlazorApp2.Model
{
    public class Customer
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int Active { get; set; }
        public bool IsActive { get { return Active == 1; } set { Active = value ? 1 : 0; } }
    }
}
