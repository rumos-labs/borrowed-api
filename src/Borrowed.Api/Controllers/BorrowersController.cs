using Borrowed.Api.Models;
using Borrowed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Borrowed.Api.Controllers;

[ApiController]
[Route("api/v1/borrowers")]
public class BorrowersController : ControllerBase
{
    private readonly IRepository<Borrower> repository;

    public BorrowersController(IRepository<Borrower> repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Borrower>> Get()
    {
        var values = repository.GetAll();

        return Ok(values);
    }

    [HttpGet("{id}")]
    public ActionResult<Borrower> Get(int id)
    {
        var value = repository.Get(id);

        if (value == null)
        {
            return NotFound();
        }

        return Ok(value);
    }

    [HttpPost]
    public ActionResult<Borrower> Post(Borrower value)
    {
        repository.Add(value);

        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    [HttpPut("{id}")]
    public ActionResult<Borrower> Put(int id, Borrower value)
    {
        if (id != value.Id)
        {
            return BadRequest();
        }

        repository.Update(value);

        return repository.Get(value.Id)!;
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        repository.Delete(id);

        return NoContent();
    }
}
