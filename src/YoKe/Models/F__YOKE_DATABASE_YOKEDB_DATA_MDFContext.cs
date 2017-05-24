using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YoKe.Models
{
    public partial class F__YOKE_DATABASE_YOKEDB_DATA_MDFContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Yoke_Database\YoKeDB_data.mdf;Integrated Security=True;Connect Timeout=30");
        //}


        public F__YOKE_DATABASE_YOKEDB_DATA_MDFContext(DbContextOptions<F__YOKE_DATABASE_YOKEDB_DATA_MDFContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__Customer__530A63AC795380F2");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(18);

                entity.Property(e => e.PassWord)
                    .HasColumnName("passWord")
                    .HasMaxLength(20);

                entity.Property(e => e.RegistDate)
                    .HasColumnName("registDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TheCustomerType).HasColumnName("theCustomerType");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(20);

                entity.HasOne(d => d.TheCustomerTypeNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.TheCustomerType)
                    .HasConstraintName("FK__Customer__theCus__1273C1CD");
            });

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__customer__530A63AC5FE54ED8");

                entity.ToTable("customerType");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.TypeName)
                    .HasColumnName("typeName")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Evaluate>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__Evaluate__530A63AC9177E152");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasMaxLength(100);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.TheProduct).HasColumnName("theProduct");

                entity.HasOne(d => d.TheProductNavigation)
                    .WithMany(p => p.Evaluate)
                    .HasForeignKey(d => d.TheProduct)
                    .HasConstraintName("FK__Evaluate__thePro__25869641");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__Orders__530A63ACA0D37465");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.OrderState).HasColumnName("orderState");

                entity.Property(e => e.OrderTime)
                    .HasColumnName("orderTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.TheCustomer).HasColumnName("theCustomer");

                entity.Property(e => e.ThePayment).HasColumnName("thePayment");

                entity.Property(e => e.TheProduct).HasColumnName("theProduct");

                entity.HasOne(d => d.TheCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TheCustomer)
                    .HasConstraintName("FK__Orders__theCusto__1920BF5C");

                entity.HasOne(d => d.ThePaymentNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ThePayment)
                    .HasConstraintName("FK__Orders__thePayme__1B0907CE");

                entity.HasOne(d => d.TheProductNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TheProduct)
                    .HasConstraintName("FK__Orders__theProdu__1A14E395");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__paymentT__530A63AC626843C7");

                entity.ToTable("paymentType");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.SmallImg)
                    .HasColumnName("smallImg")
                    .HasMaxLength(80);

                entity.Property(e => e.TypeName)
                    .HasColumnName("typeName")
                    .HasMaxLength(20);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PlaceOrder>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__placeOrd__530A63AC7E76AD1B");

                entity.ToTable("placeOrder");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.Brand)
                    .HasColumnName("brand")
                    .HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Producer)
                    .HasColumnName("producer")
                    .HasMaxLength(20);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(100);

                entity.Property(e => e.TheCustomer).HasColumnName("theCustomer");

                entity.Property(e => e.ThePaymentType).HasColumnName("thePaymentType");

                entity.Property(e => e.TheProductName)
                    .HasColumnName("theProductName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.TheCustomerNavigation)
                    .WithMany(p => p.PlaceOrder)
                    .HasForeignKey(d => d.TheCustomer)
                    .HasConstraintName("FK__placeOrde__theCu__1DE57479");

                entity.HasOne(d => d.ThePaymentTypeNavigation)
                    .WithMany(p => p.PlaceOrder)
                    .HasForeignKey(d => d.ThePaymentType)
                    .HasConstraintName("FK__placeOrde__thePa__1ED998B2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__Product__530A63AC32412D09");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.BigImg)
                    .HasColumnName("bigImg")
                    .HasMaxLength(80);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.Property(e => e.Feature)
                    .HasColumnName("feature")
                    .HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductId)
                    .HasColumnName("productID")
                    .HasMaxLength(10);

                entity.Property(e => e.ProductName)
                    .HasColumnName("productName")
                    .HasMaxLength(10);

                entity.Property(e => e.ProductState).HasColumnName("productState");

                entity.Property(e => e.SmallImg)
                    .HasColumnName("smallImg")
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<TakeOrder>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("PK__takeOrde__530A63AC1759F716");

                entity.ToTable("takeOrder");

                entity.Property(e => e.ObjId).HasColumnName("objID");

                entity.Property(e => e.TheCustomer).HasColumnName("theCustomer");

                entity.Property(e => e.ThePlaceOrder).HasColumnName("thePlaceOrder");

                entity.HasOne(d => d.TheCustomerNavigation)
                    .WithMany(p => p.TakeOrder)
                    .HasForeignKey(d => d.TheCustomer)
                    .HasConstraintName("FK__takeOrder__theCu__22AA2996");

                entity.HasOne(d => d.ThePlaceOrderNavigation)
                    .WithMany(p => p.TakeOrder)
                    .HasForeignKey(d => d.ThePlaceOrder)
                    .HasConstraintName("FK__takeOrder__thePl__21B6055D");
            });
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerType> CustomerType { get; set; }
        public virtual DbSet<Evaluate> Evaluate { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<PlaceOrder> PlaceOrder { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<TakeOrder> TakeOrder { get; set; }
    }
}