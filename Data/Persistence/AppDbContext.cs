using Microsoft.EntityFrameworkCore;
using SriDurgaHariHaraBackend.Data.Models;

namespace SriDurgaHariHaraBackend.Data.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<FinancialYear> FinancialYears { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-shiny-king-adn1m8p7-pooler.c-2.us-east-1.aws.neon.tech;Database=SriDurgaHariHaraEnterprices;Username=neondb_owner;Password=npg_Bd6jIwYRmy0Q;Ssl Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_pkey");

            entity.ToTable("company", "sdhhe_fy2025");

            entity.HasIndex(e => e.Gstin, "company_gstin_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(30)
                .HasColumnName("account_number");
            entity.Property(e => e.Address)
                .HasDefaultValueSql("'Your Full Address Here, City, Andhra Pradesh'::text")
                .HasColumnName("address");
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .HasColumnName("bank_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("'accounts@sdhhe.com'::character varying")
                .HasColumnName("email");
            entity.Property(e => e.Gstin)
                .HasMaxLength(15)
                .HasColumnName("gstin");
            entity.Property(e => e.Ifsc)
                .HasMaxLength(15)
                .HasColumnName("ifsc");
            entity.Property(e => e.LogoUrl).HasColumnName("logo_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("'SRI DURGAHARIHARA ENTERPRISES'::character varying")
                .HasColumnName("name");
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .HasColumnName("pan");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasDefaultValueSql("'+91 98765 43210'::character varying")
                .HasColumnName("phone");
            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .HasDefaultValueSql("'37'::bpchar")
                .IsFixedLength()
                .HasColumnName("state_code");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers", "sdhhe_fy2025");

            entity.HasIndex(e => e.CustomerCode, "customers_customer_code_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(20)
                .HasColumnName("customer_code");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Gstin)
                .HasMaxLength(15)
                .HasColumnName("gstin");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .HasDefaultValueSql("'37'::bpchar")
                .IsFixedLength()
                .HasColumnName("state_code");
        });

        modelBuilder.Entity<FinancialYear>(entity =>
        {
            entity.HasKey(e => e.FyCode).HasName("financial_years_pkey");

            entity.ToTable("financial_years", "sdhhe_fy2025");

            entity.HasIndex(e => e.StartDate, "financial_years_start_date_key").IsUnique();

            entity.Property(e => e.FyCode)
                .HasMaxLength(9)
                .HasColumnName("fy_code");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoices_pkey");

            entity.ToTable("invoices", "sdhhe_fy2025");

            entity.HasIndex(e => e.InvoiceNumber, "invoices_invoice_number_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AmountPaid)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amount_paid");
            entity.Property(e => e.BalanceDue)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("(total_amount - amount_paid)", true)
                .HasColumnName("balance_due");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.FyCode)
                .HasMaxLength(9)
                .HasColumnName("fy_code");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("invoice_date");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .HasColumnName("invoice_number");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'draft'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("subtotal");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("total_amount");
            entity.Property(e => e.TotalGst)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("total_gst");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("invoices_created_by_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_customer_id_fkey");

            entity.HasOne(d => d.FyCodeNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.FyCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_fy_code_fkey");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_items_pkey");

            entity.ToTable("invoice_items", "sdhhe_fy2025");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cgst)
                .HasPrecision(12, 2)
                .HasColumnName("cgst");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Discount)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("discount");
            entity.Property(e => e.GstRate)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("18")
                .HasColumnName("gst_rate");
            entity.Property(e => e.HsnSac)
                .HasMaxLength(8)
                .HasColumnName("hsn_sac");
            entity.Property(e => e.Igst)
                .HasPrecision(12, 2)
                .HasColumnName("igst");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.LineTotal)
                .HasPrecision(12, 2)
                .HasColumnName("line_total");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("1")
                .HasColumnName("quantity");
            entity.Property(e => e.Sgst)
                .HasPrecision(12, 2)
                .HasColumnName("sgst");
            entity.Property(e => e.TaxableValue)
                .HasPrecision(12, 2)
                .HasColumnName("taxable_value");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(12, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("invoice_items_invoice_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("invoice_items_product_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments", "sdhhe_fy2025");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(12, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Method)
                .HasMaxLength(20)
                .HasColumnName("method");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("payment_date");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(100)
                .HasColumnName("reference_no");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_invoice_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products", "sdhhe_fy2025");

            entity.HasIndex(e => e.Sku, "products_sku_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.GstRate)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("18.00")
                .HasColumnName("gst_rate");
            entity.Property(e => e.HsnCode)
                .HasMaxLength(8)
                .HasColumnName("hsn_code");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("sku");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Nos'::character varying")
                .HasColumnName("unit");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(12, 2)
                .HasColumnName("unit_price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", "sdhhe_fy2025");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("'user'::character varying")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
