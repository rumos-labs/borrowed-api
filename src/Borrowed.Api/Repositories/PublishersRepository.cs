using Borrowed.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Borrowed.Api.Repositories;

public class PublishersRepository : IRepository<Publisher>
{
    private readonly BorrowedDbContext context;

    public PublishersRepository(BorrowedDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Publisher> GetAll()
    {
        return context
            .Publishers
            .ToList();
    }

    public Publisher? Get(int id)
    {
        return context
            .Publishers
            .FirstOrDefault(p => p.Id == id);
    }

    public Publisher Add(Publisher value)
    {
        context.Publishers.Add(value);
        context.SaveChanges();

        return value;
    }

    public void Update(Publisher value)
    {
        context.Entry(value).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var value = Get(id);

        if (value != null)
        {
            context.Publishers.Remove(value);
            context.SaveChanges();
        }
    }
}
