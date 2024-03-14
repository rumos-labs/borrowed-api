using System.Text.Json.Serialization;

namespace Borrowed.Api.Models;

public class Borrower
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    [JsonIgnore] public  virtual List<Rental>? Rentals { get; set; } = [];
}