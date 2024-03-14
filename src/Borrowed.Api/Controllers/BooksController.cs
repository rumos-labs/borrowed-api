using Borrowed.Api.Models;
using Borrowed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Borrowed.Api.Controllers;

[ApiController]
[Route("api/v1/books")]
public class BooksController : ControllerBase
{
    private readonly IRepository<Book> booksRepository;
    private readonly IRepository<Borrower> borrowersRepository;
    private readonly IRepository<Publisher> publishersRepository;

    public BooksController(IRepository<Book> booksRepository, IRepository<Borrower> borrowersRepository, IRepository<Publisher> publishersRepository)
    {
        this.booksRepository = booksRepository;
        this.borrowersRepository = borrowersRepository;
        this.publishersRepository = publishersRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> Get()
    {
        var books = booksRepository.GetAll();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public ActionResult<Book> Get(int id)
    {
        var book = booksRepository.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> Post(Book book)
    {
        booksRepository.Add(book);

        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public ActionResult<Book> Put(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        booksRepository.Update(book);

        return booksRepository.Get(book.Id)!;
    }

    [HttpPatch("{id}")]
    public ActionResult<Book> Patch(int id, int publisherId)
    {
        var book = booksRepository.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        var publisher = publishersRepository.Get(publisherId);

        if (publisher == null)
        {
            return NotFound();
        }

        book.Publisher = publisher;

        booksRepository.Update(book);

        return booksRepository.Get(book.Id)!;
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {          
        booksRepository.Delete(id);
       
        return NoContent();
    }

    [HttpPost("{id}/rent/{borrowerId}")]
    public ActionResult<Book> Rent(int id, int borrowerId, Rental rental)
    {
        var book = booksRepository.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        var borrower = borrowersRepository.Get(borrowerId);

        if (borrower == null)
        {
            return NotFound();
        }

        rental.Borrower = borrower;
        rental.Book = book;

        book.Rentals!.Add(rental);

        booksRepository.Update(book);

        return Ok(book);
    }

    [HttpPost("{id}/return/{rentalId}")]
    public ActionResult<Book> Rent(int id, int rentalId)
    {
        var book = booksRepository.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        var rental = book.Rentals!.FirstOrDefault(r => r.Id == rentalId);

        if (rental == null)
        {
            return NotFound();
        }

        rental.ReturnDate = DateTime.Now;

        booksRepository.Update(book);

        return Ok(book);
    }
}
