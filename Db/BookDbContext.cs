using System;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Db
{
    public sealed class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BookLibrary.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book[]
                {
                    new Book {Id = Guid.NewGuid(), Name = "Tom", Author = "Tom"},
                    new Book {Id = Guid.NewGuid(), Name = "Adam", Author = "Adam"}
                });
        }
    }
}