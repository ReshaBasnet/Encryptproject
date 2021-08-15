//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace EncryptCLoud.Models
//{
//    public partial class encryptappContext : DbContext
//    {
//        public encryptappContext()
//        {
//        }

//        public encryptappContext(DbContextOptions<encryptappContext> options)
//            : base(options)
//        {
//        }

//        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
//        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
//        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
//        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
//        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
//        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
//        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
//        public virtual DbSet<File> File { get; set; }
//        public virtual DbSet<Sharedfile> Sharedfile { get; set; }
//        public virtual DbSet<Userfriend> Userfriend { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;;Database=encryptapp;Integrated Security=True");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<AspNetRoleClaims>(entity =>
//            {
//                entity.HasIndex(e => e.RoleId);

//                entity.Property(e => e.RoleId).IsRequired();

//                entity.HasOne(d => d.Role)
//                    .WithMany(p => p.AspNetRoleClaims)
//                    .HasForeignKey(d => d.RoleId);
//            });

//            modelBuilder.Entity<AspNetRoles>(entity =>
//            {
//                entity.HasIndex(e => e.NormalizedName)
//                    .HasName("RoleNameIndex")
//                    .IsUnique()
//                    .HasFilter("([NormalizedName] IS NOT NULL)");

//                entity.Property(e => e.Name).HasMaxLength(256);

//                entity.Property(e => e.NormalizedName).HasMaxLength(256);
//            });

//            modelBuilder.Entity<AspNetUserClaims>(entity =>
//            {
//                entity.HasIndex(e => e.UserId);

//                entity.Property(e => e.UserId).IsRequired();

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserClaims)
//                    .HasForeignKey(d => d.UserId);
//            });

//            modelBuilder.Entity<AspNetUserLogins>(entity =>
//            {
//                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

//                entity.HasIndex(e => e.UserId);

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.ProviderKey).HasMaxLength(128);

//                entity.Property(e => e.UserId).IsRequired();

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserLogins)
//                    .HasForeignKey(d => d.UserId);
//            });

//            modelBuilder.Entity<AspNetUserRoles>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.RoleId });

//                entity.HasIndex(e => e.RoleId);

//                entity.HasOne(d => d.Role)
//                    .WithMany(p => p.AspNetUserRoles)
//                    .HasForeignKey(d => d.RoleId);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserRoles)
//                    .HasForeignKey(d => d.UserId);
//            });

//            modelBuilder.Entity<AspNetUserTokens>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.Name).HasMaxLength(128);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserTokens)
//                    .HasForeignKey(d => d.UserId);
//            });

//            modelBuilder.Entity<AspNetUsers>(entity =>
//            {
//                entity.HasIndex(e => e.NormalizedEmail)
//                    .HasName("EmailIndex");

//                entity.HasIndex(e => e.NormalizedUserName)
//                    .HasName("UserNameIndex")
//                    .IsUnique()
//                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

//                entity.Property(e => e.Email).HasMaxLength(256);

//                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

//                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

//                entity.Property(e => e.UserName).HasMaxLength(256);
//            });

//            modelBuilder.Entity<File>(entity =>
//            {
//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.Path)
//                    .IsRequired()
//                    .HasColumnName("path");

//                entity.Property(e => e.UploadDate)
//                    .IsRequired()
//                    .HasColumnName("upload_date")
//                    .IsRowVersion();

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasColumnName("user_id")
//                    .HasMaxLength(450);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.File)
//                    .HasForeignKey(d => d.UserId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_File_AspNetUsers");
//            });

//            modelBuilder.Entity<Sharedfile>(entity =>
//            {
//                entity.ToTable("sharedfile");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.FriendId)
//                    .IsRequired()
//                    .HasColumnName("friend_id")
//                    .HasMaxLength(450);

//                entity.Property(e => e.ImageId).HasColumnName("image_id");

//                entity.HasOne(d => d.Friend)
//                    .WithMany(p => p.Sharedfile)
//                    .HasForeignKey(d => d.FriendId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_sharedfile_AspNetUsers");

//                entity.HasOne(d => d.Image)
//                    .WithMany(p => p.Sharedfile)
//                    .HasForeignKey(d => d.ImageId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_sharedfile_File");
//            });

//            modelBuilder.Entity<Userfriend>(entity =>
//            {
//                entity.ToTable("userfriend");

//                entity.Property(e => e.Id).HasColumnName("id");

//                entity.Property(e => e.FriendId)
//                    .IsRequired()
//                    .HasColumnName("friend_id")
//                    .HasMaxLength(450);

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasColumnName("user_id")
//                    .HasMaxLength(450);

//                entity.HasOne(d => d.Friend)
//                    .WithMany(p => p.UserfriendFriend)
//                    .HasForeignKey(d => d.FriendId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_userfriend_AspNetUsers1");

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.UserfriendUser)
//                    .HasForeignKey(d => d.UserId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_userfriend_AspNetUsers");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
