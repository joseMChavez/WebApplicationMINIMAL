using Microsoft.EntityFrameworkCore;
using WebApplicationMINIMAL.Data;
using WebApplicationMINIMAL.Model;
using WebApplicationMINIMAL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<ITodoService, ToDoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/ToDo", async (ITodoService service) =>
{
    return await Task.FromResult(Results.Ok(
        await service.Get()
        ));
}).WithName("GetToDo");
app.MapGet("/ToDo/{id:Guid}", async (ITodoService service,Guid id) =>
{
    
    if (await service.Get(id) is Todo todo)
    {
        return Results.Ok(todo);
    }
    return Results.NotFound("Not Fount");

}).WithName("GetByIdToDo");

/// <summary>
/// Add Item
/// </summary>
app.MapPost("/Todo", async (ITodoService service, Todo todo) =>
{
  
        var result = await service.Create(todo);
        if (result != null)
        {
          return  Results.Ok(result);
        }
       return Results.BadRequest();
    

}).WithName("AddTodo");
/// <summary>
/// Update Item
/// </summary>
app.MapPut("/Todo", async (ITodoService service, Todo todo) =>
{

    var result = await service.Update(todo);
    if (result != null)
    {
        return Results.Ok(result);
    }
    return Results.BadRequest();


}).WithName("UpdateTodo");
/// <summary>
/// Update Item
/// </summary>
app.MapPut("/CompleteTask", async (ITodoService service, Guid id,bool isComplete) =>
{
    if (await service.Get(id) is Todo todo)
    {
        todo.IsComplete = isComplete;
        var result = await service.Update(todo);
        if (result != null)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest();
    }
    return Results.NotFound();
}).WithName("CompleteTask");
/// <summary>
/// delete Item
/// </summary>
app.MapDelete("/Todo", async (ITodoService service, Guid id) =>
{

    var success = await service.Delete(id);
    if (success)
    {
        return Results.Ok("Correcto");
    }
    return Results.BadRequest();


}).WithName("DeleteTodo");
app.Run();
//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//       new WeatherForecast
//       (
//           DateTime.Now.AddDays(index),
//           Random.Shared.Next(-20, 55),
//           summaries[Random.Shared.Next(summaries.Length)]
//       ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeather");
//app.MapPost("/weatherforecast", ( string r) => {
//    summaries.Append(r);

//}).WithName("PostWeathenr");
//app.Run();

//internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
//class Todo
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public bool IsComplete { get; set; }
//}

//class TodoDb : DbContext
//{
//    public TodoDb(DbContextOptions<TodoDb> options)
//        : base(options) { }

//    public DbSet<Todo> Todos => Set<Todo>();
//}
