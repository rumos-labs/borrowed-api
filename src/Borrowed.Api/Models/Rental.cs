using System.Text.Json.Serialization;

namespace Borrowed.Api.Models;

public class Rental
{
    public int Id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    [JsonIgnore] public virtual Book? Book { get; set; } = default!;

    [JsonIgnore] public virtual Borrower? Borrower { get; set; } = default!;
}
