using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CustomerWebsite.Service.Modals
{
    public partial class WideWorldImportersContext : DbContext
    {
        public WideWorldImportersContext()
        {
        }

        public WideWorldImportersContext(DbContextOptions<WideWorldImportersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=rbekhit.database.windows.net; Database=WideWorldImporters; User ID=usr587;Password=UGxCWh5NgfVb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_100_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Customers", "Website");

                entity.Property(e => e.AlternateContact).HasMaxLength(50);

                entity.Property(e => e.BuyingGroupName).HasMaxLength(50);

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CustomerCategoryName).HasMaxLength(50);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DeliveryMethod).HasMaxLength(50);

                entity.Property(e => e.DeliveryRun).HasMaxLength(5);

                entity.Property(e => e.FaxNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PrimaryContact).HasMaxLength(50);

                entity.Property(e => e.RunPosition).HasMaxLength(5);

                entity.Property(e => e.WebsiteUrl)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("WebsiteURL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
