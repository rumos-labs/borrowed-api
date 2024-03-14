using Borrowed.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Borrowed.Api.Repositories;

public class BooksRepository : IRepository<Book>
{
    private readonly BorrowedDbContext context;

    public BooksRepository(BorrowedDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Book> GetAll()
    {
        return context
            .Books
            .Include(b => b.Rentals)
            .Include(b => b.Publisher)
            .ToList();
    }

    public Book? Get(int id)
    {
        return context
            .Books
            .Include(b => b.Rentals)
            .Include(b => b.Publisher)
            .FirstOrDefault(b => b.Id == id);
    }

    public Book Add(Book value)
    {
        context.Books.Add(value);
        context.SaveChanges();

        return value;
    }

    public void Update(Book value)
    {
        context.Entry(value).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var value = Get(id);

        if (value != null)
        {
            context.Books.Remove(value);
            context.SaveChanges();
        }
    }
}
