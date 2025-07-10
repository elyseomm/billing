using Billing.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Billing.Core
{
    public class BillingContext(DbContextOptions<BillingContext> opt) : DbContext(opt)
    {
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Product> Products { set; get; }

        public DbSet<Invoice> Invoices { set; get; }
        public DbSet<InvoiceLine> InvoiceLines { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(a =>
            {
                a.HasKey(e => e.Id);
                a.Property(x => x.Id).IsRequired().HasMaxLength(36).ValueGeneratedNever();
                a.Property(x => x.Name).HasMaxLength(100).IsRequired();
                a.Property(x => x.Email).HasMaxLength(100).IsRequired();
                a.Property(x => x.Address).HasMaxLength(150).IsRequired();
                a.Property(x => x.Active).IsRequired();
            });

            modelBuilder.Entity<Product>(a =>
            {
                a.HasKey(e => e.Id);
                a.Property(x => x.Id).IsRequired().HasMaxLength(36).ValueGeneratedNever();
                a.Property(x => x.ProductName).HasMaxLength(100).IsRequired();
                a.Property(x => x.Active).IsRequired();
            });

            modelBuilder.Entity<Invoice>(a =>
            {
                a.HasKey(e => e.Id);
                a.Property(x => x.Id).IsRequired();
                a.Property(x => x.CustomerId).HasMaxLength(36).IsRequired();
                a.Property(x => x.Name).HasMaxLength(100);
                a.Property(x => x.InvoiceNumber).HasMaxLength(100);
                a.Property(x => x.CreatedAt);
                a.Property(x => x.Date);
                a.Property(x => x.DueDate);
                
                a.Property(x => x.InvoiceDate);
                a.Property(x => x.InvoiceAmount);
                a.Property(x => x.TotalAmount);

                a.Property(x => x.BillingLines);
                a.Property(x => x.CurrencyCode).HasMaxLength(5);
                a.Property(x => x.Currency).HasMaxLength(5);
                a.Property(x => x.Active).IsRequired();

            });

            modelBuilder.Entity<InvoiceLine>(a =>
            {
                a.HasKey(e => e.Id);
                a.Property(x => x.Id).IsRequired();
                a.Property(x => x.ProductId).HasMaxLength(36).IsRequired();
                a.Property(x => x.InvoiceId);
                a.Property(x => x.Quantity);
                a.Property(x => x.UnitPrice).HasDefaultValue(0);
                a.Property(x => x.SubTotal).HasDefaultValue(0);
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();    // Dados Iniciais
        }
    }
}
