using Borrowed.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Borrowed.Api;

public class BorrowedDbContext : DbContext
{
    public BorrowedDbContext(DbContextOptions<BorrowedDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }

    public DbSet<Rental> Rentals { get; set; }

    public DbSet<Borrower> Borrowers { get; set; }

    public DbSet<Publisher> Publishers { get; set; }
}
