namespace BlazorApp2.Model
{
    public class Product
    {
        public string? Id { get; set; }
        public string? ProductName { get; set; }
        public int Active { get; set; }
        public bool IsActive { get { return Active == 1; } set { Active = value ? 1 : 0; } }

        public string Message { get; set; }
    }
}
