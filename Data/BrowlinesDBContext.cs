using Browlines_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Browlines_API.Data
{
    public class BrowlinesDBContext : DbContext
    {        
        protected BrowlinesDBContext()
        {
        }

        public BrowlinesDBContext(DbContextOptions<BrowlinesDBContext> options) : base(options)
        {
        }

     
        public virtual DbSet<User_Model> Users { get; set; }       
        public virtual DbSet<Customer_Model> Customers { get; set; }
        public virtual DbSet<Champion_Model> Champions { get; set; }
        public virtual DbSet<Procedures_Model> Procedures { get; set; }
        public virtual DbSet<Transaction_Model> Transactions { get; set; }
        public virtual DbSet<Transaction_Procedures_Model> TransactionProcedures { get; set; }


        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrowlinesDBContext).Assembly);
         }*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseMySql("Name=ConnectionStrings:DefaultConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("12.3.2-mariadb"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
           .UseCollation("utf8mb4_uca1400_ai_ci")
           .HasCharSet("utf8mb4");

            modelBuilder.Entity<Procedures_Model>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("procedures");

                entity.HasIndex(e => e.CreatedBy, "procedures_ibfk_1");
                entity.HasIndex(e => e.UpdatedBy, "procedures_ibfk_2");

                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProcedureCreatedByRecords)
                  .HasForeignKey(d => d.CreatedBy)
                  .HasConstraintName("procedures_ibfk_1");

                entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProcedureUpdatedByRecords)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("procedures_ibfk_2");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                  .HasColumnType("int(11)")
                  .HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by");
                entity.Property(e => e.Service)
                     .HasMaxLength(100)
                    .HasColumnName("service");
                entity.Property(e => e.Procedure)
                    .HasMaxLength(100)
                    .HasColumnName("procedure");
                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");
                entity.Property(e => e.Duration)
                    .HasColumnName("duration");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("current_timestamp()")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });


            modelBuilder.Entity<Transaction_Model>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("transactions");

                entity.HasIndex(e => e.CustomerId, "transactions_ibfk_1");
                entity.HasIndex(e => e.Id, "transactions_ibfk_2");

                entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.CustomerRecords)
                  .HasForeignKey(d => d.CustomerId)
                  .HasConstraintName("transactions_ibfk_1");

                entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
                entity.Property(e => e.CustomerId)
                 .HasColumnType("int(11)")
                 .HasColumnName("customer_id");
                entity.Property(e => e.Status)
                  .HasMaxLength(50)
                  .HasColumnName("status");
                entity.Property(e => e.PaymentStatus)
                 .HasMaxLength(50)
                 .HasColumnName("payment_status");
                entity.Property(e => e.TotalAmount)
                 .HasPrecision(10, 2)
                 .HasColumnName("total_amount");
                entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            });


            modelBuilder.Entity<Transaction_Procedures_Model>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("transaction_procedures");

                entity.HasIndex(e => e.TransactionId, "transaction_procedure_ibfk_1");
                entity.HasIndex(e => e.ChampionId, "transaction_procedure_ibfk_2");

                entity.HasOne(d => d.TransactionNavigation).WithMany(p => p.TransactionProcedureRecords)
                 .HasForeignKey(d => d.TransactionId)
                 .HasConstraintName("transaction_procedure_ibfk_1");
                entity.HasOne(d => d.ChampionNavigation).WithMany(p => p.TransactionProcedureChampionRecord)
                .HasForeignKey(d => d.ChampionId)
                .HasConstraintName("transaction_procedure_ibfk_2");

                entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
                entity.Property(e => e.TransactionId)
                .HasColumnType("int(11)")
                .HasColumnName("transaction_id");
                entity.Property(e => e.ChampionId)
                .HasColumnType("int(11)")
                .HasColumnName("champion_id");
                entity.Property(e => e.Service)
                .HasMaxLength(150)
                .HasColumnName("service");
                entity.Property(e => e.Procedure)
                .HasMaxLength(150)
                .HasColumnName("procedure");
                entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
                entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
                entity.Property(e => e.ScheduleDate)
                .HasColumnType("date")
                .HasColumnName("schedule_date");
                entity.Property(e => e.ScheduleTime)
                .HasColumnType("time")
                .HasColumnName("schedule_time");
            });



                modelBuilder.Entity<User_Model>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("users");
                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.About)
                    .HasColumnType("text")
                    .HasColumnName("about");
                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address");
                entity.Property(e => e.Birthdate).HasColumnName("birthdate");
                entity.Property(e => e.ContactNo)
                    .HasMaxLength(20)
                    .HasColumnName("contact_no");
                entity.Property(e => e.Img).HasColumnName("img");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Role)
                 .HasMaxLength(30)
                 .HasColumnName("role");
                entity.Property(e => e.Pass)
                    .HasMaxLength(50)
                    .HasColumnName("pass");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Customer_Model>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("customers");

                entity.HasIndex(e => e.CreatedBy, "customer_ibfk_1");

                entity.HasIndex(e => e.UpdatedBy, "customer_ibfk_2");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");
                entity.Property(e => e.Allergies)
                    .HasMaxLength(255)
                    .HasColumnName("allergies");
                entity.Property(e => e.Birthdate).HasColumnName("birthdate");
                entity.Property(e => e.Complaints)
                    .HasMaxLength(255)
                    .HasColumnName("complaints");
                entity.Property(e => e.ContactNo)
                    .HasMaxLength(50)
                    .HasColumnName("contact_no");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("current_timestamp()")
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by");
                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
                entity.Property(e => e.Notes)
                    .HasMaxLength(255)
                    .HasColumnName("notes");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerCreatedByRecords)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("customer_ibfk_1");

                entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.CustomerUpdatedByRecords)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("customer_ibfk_2");
            });

            modelBuilder.Entity<Champion_Model>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("champions");
                entity.HasIndex(e => e.CreatedBy, "champions_ibfk_1");
                entity.HasIndex(e => e.UpdateBy, "champions_ibfk_2");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Specialization)
                    .HasColumnType("text")
                    .HasColumnName("specialization");
                entity.Property(e => e.AvailableFrom)
                    .HasColumnType("time")
                    .HasColumnName("available_from");
                entity.Property(e => e.AvailableTo)
                .HasColumnType("time")
                .HasColumnName("available_to");                
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");
                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by");
                //entity.Property(e => e.Img).HasColumnName("img");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Specialization)
                    .HasMaxLength(150)
                    .HasColumnName("specialization");
                entity.Property(e => e.UpdateBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("update_by");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ChampionCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("champions_ibfk_1");

                entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.ChampionUpdateByNavigations)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("champions_ibfk_2");
            });


        }
     }
}
