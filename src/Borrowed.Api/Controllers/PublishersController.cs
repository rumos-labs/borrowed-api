using Borrowed.Api.Models;
using Borrowed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Borrowed.Api.Controllers;

[ApiController]
[Route("api/v1/publishers")]
public class PublishersController : ControllerBase
{
    private readonly IRepository<Publisher> repository;

    public PublishersController(IRepository<Publisher> repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Publisher>> Get()
    {
        var values = repository.GetAll();

        return Ok(values);
    }

    [HttpGet("{id}")]
    public ActionResult<Publisher> Get(int id)
    {
        var value = repository.Get(id);

        if (value == null)
        {
            return NotFound();
        }

        return Ok(value);
    }

    [HttpPost]
    public ActionResult<Publisher> Post(Publisher value)
    {
        repository.Add(value);

        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    [HttpPut("{id}")]
    public ActionResult<Publisher> Put(int id, Publisher value)
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
