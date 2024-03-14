using System.Text.Json.Serialization;

namespace Borrowed.Api.Models;

public class Publisher
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    [JsonIgnore] public virtual List<Book>? Books { get; set; } = [];
}