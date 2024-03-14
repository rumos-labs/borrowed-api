using Borrowed.Api;
using Borrowed.Api.Models;
using Borrowed.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BorrowedDbContext>(options =>
{
    options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Database=Borrowed;Integrated Security=sspi;");
    //options.UseSqlServer("Data Source=borrowed-db.rumos.cloud;Database=Borrowed;User Id=sa;Password=rumos123!;TrustServerCertificate=true;");
});

builder.Services.AddScoped<IRepository<Book>, BooksRepository>();
builder.Services.AddScoped<IRepository<Borrower>, BorrowersRepository>();
builder.Services.AddScoped<IRepository<Publisher>, PublishersRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
