using Microsoft.EntityFrameworkCore;

namespace MinhTienHairSalon.Models
{
    public class DBContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AdminAccount> AdminAccounts { get; init; }
        public DbSet<Service> Services { get; init; }
        public DbSet<Product> Products { get; init; }
        public DbSet<Employee> Employees { get; init; }
        public DbSet<Reservation> Reservations { get; init; }
        public DbSet<ReservationDetail> ReservationDetails { get; init; }
        public DbSet<Invoice> Invoices { get; init; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; init; }
        public DbSet<Message> Messages { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdminAccount>();
            modelBuilder.Entity<Service>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Employee>();
            modelBuilder.Entity<Reservation>();
            modelBuilder.Entity<ReservationDetail>();
            modelBuilder.Entity<Invoice>();
            modelBuilder.Entity<InvoiceDetail>();
            modelBuilder.Entity<Message>();

            modelBuilder.Entity<ReservationDetail>()
            .HasKey(r => new { r.ServiceID, r.ReservationID });

            modelBuilder.Entity<InvoiceDetail>()
            .HasKey(i => new { i.InvoiceId, i.ProductId });
        }
    }
}
