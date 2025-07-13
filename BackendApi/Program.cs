using BackendApi.Database;
using BackendApi.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IProdutosService, ProdutosService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source = Produtos.db"));

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


List<Produto> produtos = new List<Produto>()
{
    new Produto() {Id = 1, Nome = "Mouse sem fio", Preco = 99.90M, Estoque = 50},
    new Produto() {Id = 2, Nome = "Teclado", Preco = 49.90M, Estoque = 30}
};

app.MapGet("/produtos", () => 
{
    return produtos;
});

app.MapGet("/produtos/{id}", (int id) => 
{ 
    var produto = produtos.FirstOrDefault(x => x.Id == id);
    return produto is not null
    ? Results.Ok(produto)
    : Results.NotFound($"Produto com ID {id} não encontrado.");
});

app.MapPost("/produtos", (Produto novoProduto) => 
{ 
    produtos.Add(novoProduto);
    return Results.Created();
});

app.MapPut("/produtos/{id}", (int id, Produto produtoAtualizado) => 
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);
    if (produto is null)
    {
        return Results.NotFound($"Produto com ID {id} não encontrado.");
    }

    produto.Nome = produtoAtualizado.Nome;
    produto.Preco = produtoAtualizado.Preco;
    produto.Estoque = produtoAtualizado.Estoque;

    return Results.Ok(produto);

});

app.MapDelete("produtos/{id}", (int id) => 
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);
    if (produto is null)
    {
        return Results.NotFound($"Produto com ID {id} não encontrado.");
    }

    produtos.Remove(produto);
    return Results.NoContent();

});


app.Run();


public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }

} 
