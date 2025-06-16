using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BookStoreDBContext :DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options)
        : base(options)
        {
        }

        // DbSet properties
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // BookCate: Composite key
            modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bills)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User1)
                .WithMany(u => u.ChatsAsUser1)
                .HasForeignKey(c => c.Participants1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User2)
                .WithMany(u => u.ChatsAsUser2)
                .HasForeignKey(c => c.Participants2)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CateId });

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCates)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCates)
                .HasForeignKey(bc => bc.CateId);

            // BillDetail: Composite key
            modelBuilder.Entity<BillDetail>()
                .HasKey(bd => new { bd.BillId, bd.BookId });

            modelBuilder.Entity<BillDetail>()
                .HasOne(bd => bd.Bill)
                .WithMany(b => b.BillDetails)
                .HasForeignKey(bd => bd.BillId);

            modelBuilder.Entity<BillDetail>()
                .HasOne(bd => bd.Book)
                .WithMany(b => b.BillDetails)
                .HasForeignKey(bd => bd.BookId);

            // CartDetail: Composite key
            modelBuilder.Entity<CartDetail>()
                .HasKey(cd => new { cd.CartId, cd.BookId });

            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(cd => cd.CartId);

            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Book)
                .WithMany(b => b.CartDetails)
                .HasForeignKey(cd => cd.BookId);
            modelBuilder.Entity<Chat>()
            .HasOne(c => c.User1)
            .WithMany(u => u.ChatsAsUser1) // define this in ApplicationUser
            .HasForeignKey(c => c.Participants1)
            .OnDelete(DeleteBehavior.Restrict); // prevent cascading loop

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User2)
                .WithMany(u => u.ChatsAsUser2) // define this in ApplicationUser
                .HasForeignKey(c => c.Participants2)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>()
            .Property(b => b.Price)
            .HasPrecision(18, 2);

            modelBuilder.Entity<CartDetail>()
                .Property(cd => cd.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<BillDetail>()
                .Property(bd => bd.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Bill>()
                .Property(b => b.Total)
                .HasPrecision(18, 2);

        }

    }
}
   

