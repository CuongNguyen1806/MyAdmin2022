using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyAdmin2022.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<ContactCategory> ContactCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyAdminDB;Persist Security Info=True;User ID=sa;Password=123456;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Account__536C85E5D8BD64AA");

                entity.ToTable("Account");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.AccountCategoryId)
                    .HasMaxLength(50)
                    .HasColumnName("AccountCategoryID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Thumb).HasMaxLength(255);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.ArticleCategoryId).HasColumnName("ArticleCategoryID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.ArticleCategory)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.ArticleCategoryId)
                    .HasConstraintName("FK__Article__Article__1920BF5C");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK__Article__CreateB__239E4DCF");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.ToTable("ArticleCategory");

                entity.Property(e => e.ArticleCategoryId).HasColumnName("ArticleCategoryID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK__ArticleCa__Creat__1FCDBCEB");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.ApproveBy).HasMaxLength(50);

                entity.Property(e => e.ContactCategoryId).HasColumnName("ContactCategoryID");

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.HasOne(d => d.ApproveByNavigation)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ApproveBy)
                    .HasConstraintName("FK__Contact__Approve__44FF419A");

                entity.HasOne(d => d.ContactCategory)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactCategoryId)
                    .HasConstraintName("FK__Contact__Contact__1CF15040");
            });

            modelBuilder.Entity<ContactCategory>(entity =>
            {
                entity.ToTable("ContactCategory");

                entity.Property(e => e.ContactCategoryId).HasColumnName("ContactCategoryID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ContactCategories)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK__ContactCa__Creat__412EB0B6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
