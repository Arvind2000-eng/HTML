using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HelperLand.Models;

#nullable disable

namespace HelperLand.Data
{
    public partial class HelperLand_DatabaseContext : DbContext
    {
        public HelperLand_DatabaseContext()
        {
        }

        public HelperLand_DatabaseContext(DbContextOptions<HelperLand_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<FavoriteAndBlocked> FavoriteAndBlockeds { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
        public virtual DbSet<ServiceRequestAddress> ServiceRequestAddresses { get; set; }
        public virtual DbSet<ServiceRequestExtra> ServiceRequestExtras { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<Zipcode> Zipcodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=DARWIN;Database=HelperLand_Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__City__StateId__286302EC");
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.HasKey(e => e.ContactUsId);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Subject).HasMaxLength(500);

                entity.Property(e => e.UploadFileName).HasMaxLength(100);
            });

            modelBuilder.Entity<FavoriteAndBlocked>(entity =>
            {
                entity.ToTable("FavoriteAndBlocked");

                entity.HasOne(d => d.TargetUser)
                    .WithMany(p => p.FavoriteAndBlockedTargetUsers)
                    .HasForeignKey(d => d.TargetUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FavoriteA__Targe__300424B4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavoriteAndBlockedUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FavoriteA__UserI__2F10007B");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.Comments).HasMaxLength(2000);

                entity.Property(e => e.Friendly).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.OnTimeArrival).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.QualityOfService).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.RatingDate).HasColumnType("datetime");

                entity.Property(e => e.Ratings).HasColumnType("decimal(5, 0)");

                entity.HasOne(d => d.RatingFromNavigation)
                    .WithMany(p => p.RatingRatingFromNavigations)
                    .HasForeignKey(d => d.RatingFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__RatingFr__3F466844");

                entity.HasOne(d => d.RatingToNavigation)
                    .WithMany(p => p.RatingRatingToNavigations)
                    .HasForeignKey(d => d.RatingTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__RatingTo__403A8C7D");

                entity.HasOne(d => d.ServiceRequest)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.ServiceRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rating__ServiceR__3E52440B");
            });

            modelBuilder.Entity<ServiceRequest>(entity =>
            {
                entity.ToTable("ServiceRequest");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Discount).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Distance).HasColumnType("decimal(9, 0)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentTransactionRefNo).HasMaxLength(50);

                entity.Property(e => e.RefundedAmount).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.ServiceHourlyRate).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.ServiceStartDate).HasColumnType("datetime");

                entity.Property(e => e.SpacceptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("SPAcceptedDate");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.ServiceRequestServiceProviders)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .HasConstraintName("FK__ServiceRe__Servi__38996AB5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ServiceRequestUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceRe__UserI__37A5467C");
            });

            modelBuilder.Entity<ServiceRequestAddress>(entity =>
            {
                entity.ToTable("ServiceRequestAddress");

                entity.Property(e => e.AddressLine1).HasMaxLength(200);

                entity.Property(e => e.AddressLine2).HasMaxLength(200);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.HasOne(d => d.ServiceRequest)
                    .WithMany(p => p.ServiceRequestAddresses)
                    .HasForeignKey(d => d.ServiceRequestId)
                    .HasConstraintName("FK__ServiceRe__Servi__4316F928");
            });

            modelBuilder.Entity<ServiceRequestExtra>(entity =>
            {
                entity.ToTable("ServiceRequestExtra");

                entity.HasOne(d => d.ServiceRequest)
                    .WithMany(p => p.ServiceRequestExtras)
                    .HasForeignKey(d => d.ServiceRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceRe__Servi__45F365D3");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Test");

                entity.Property(e => e.TestName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.BankTokenId).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PaymentGatewayUserRef).HasMaxLength(200);

                entity.Property(e => e.TaxNo).HasMaxLength(50);

                entity.Property(e => e.UserProfilePicture).HasMaxLength(200);

                entity.Property(e => e.ZipCode).HasMaxLength(20);
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK_UserAddresses");

                entity.ToTable("UserAddress");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.AddressLine2).HasMaxLength(200);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.State).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAddre__UserI__4AB81AF0");
            });

            modelBuilder.Entity<Zipcode>(entity =>
            {
                entity.ToTable("Zipcode");

                entity.Property(e => e.ZipcodeValue)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Zipcodes)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Zipcode__CityId__4D94879B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
