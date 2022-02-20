using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FptBookNew1.Models
{
    public partial class ModelDatabase : DbContext
    {
        public ModelDatabase()
            : base("name=ModelDatabase")
        {
        }

        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<orderDetail> orderDetails { get; set; }
        public virtual DbSet<order> orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<account>()
                .HasMany(e => e.feedbacks)
                .WithRequired(e => e.account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<account>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<author>()
                .Property(e => e.authorID)
                .IsUnicode(false);

            modelBuilder.Entity<author>()
                .Property(e => e.authorName)
                .IsUnicode(false);

            modelBuilder.Entity<author>()
                .HasMany(e => e.books)
                .WithRequired(e => e.author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<book>()
                .Property(e => e.bookID)
                .IsUnicode(false);

            modelBuilder.Entity<book>()
                .Property(e => e.categoryID)
                .IsUnicode(false);

            modelBuilder.Entity<book>()
                .Property(e => e.authorID)
                .IsUnicode(false);

            modelBuilder.Entity<book>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<book>()
                .HasMany(e => e.orderDetails)
                .WithRequired(e => e.book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<category>()
                .Property(e => e.categoryID)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.categoryName)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.books)
                .WithRequired(e => e.category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<feedback>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<orderDetail>()
                .Property(e => e.bookID)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .HasMany(e => e.orderDetails)
                .WithRequired(e => e.order)
                .WillCascadeOnDelete(false);
        }
    }
}
