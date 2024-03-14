using Borrowed.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Borrowed.Api.Repositories;

public class BorrowersRepository : IRepository<Borrower>
{
    private readonly BorrowedDbContext context;

    public BorrowersRepository(BorrowedDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Borrower> GetAll()
    {
        return context
            .Borrowers
            .ToList();
    }

    public Borrower? Get(int id)
    {
        return context
            .Borrowers
            .FirstOrDefault(b => b.Id == id);
    }

    public Borrower Add(Borrower value)
    {
        context.Borrowers.Add(value);
        context.SaveChanges();

        return value;
    }

    public void Update(Borrower value)
    {
        context.Entry(value).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var borrower = Get(id);

        if (borrower != null)
        {
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
        }
    }
}
