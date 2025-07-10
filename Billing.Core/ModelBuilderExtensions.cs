using Microsoft.EntityFrameworkCore;

namespace Billing.Core
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Customers

            //modelBuilder.Entity<Customer>().HasData(
            //    new Customer() { Id = CustomConverter.NewGuid(), Name = "Elyseo M. Mesquita Seed", Email = "versatiletech2021@gmail.com", Address = "Street 23 - nr 21", Active = 1 }
            //);

            #endregion

            #region Products

            //modelBuilder.Entity<Product>().HasData(
            //    new Product() { Id = Guid.NewGuid(), ProductName = "Product 1" }
            //);

            #endregion
        }
    }
}
