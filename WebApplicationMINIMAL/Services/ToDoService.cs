
using Microsoft.EntityFrameworkCore;
using WebApplicationMINIMAL.Data;
using WebApplicationMINIMAL.Model;
namespace WebApplicationMINIMAL.Services
{
    public interface ITodoService
    {

        Task<List<Todo>> Get();
        Task<Todo> Get(Guid id);
        Task<bool> Delete(Guid id);
        Task<Todo> Create(Todo item);
        Task<Todo> Update(Todo item);

    }

    public class ToDoService : ITodoService
    {
        private readonly TodoDb db;
        
        public ToDoService(TodoDb todoDb)
        {
            db = todoDb;
        }

        public ToDoService()
        {
        }

        public async Task<List<Todo>> Get()
        {
            try
            {
                return await Task.FromResult(
                      await db.Todos.ToListAsync()
                      );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            return new List<Todo>();
        }

        public async Task<Todo?> Get(Guid id)
        {
            try
            {
                Todo? result = new(); 
                result = await db.Todos.FindAsync(id);
                if (result==null)
                {
                    result = new();
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                if (await db.Todos.FindAsync(id) is Todo todo)
                {
                    db.Todos.Remove(todo);
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Todo?> Create(Todo? item)
        {
            try
            {

                db.Todos.Add(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            return null;
        }

        public async Task<Todo?> Update(Todo item)
        {
            try
            {
                var todo = await db.Todos.FindAsync(item.Id);

                if (todo is null) return new();

                todo.Name = item.Name;
                todo.IsComplete = item.IsComplete;

                await db.SaveChangesAsync();

                return todo;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            return null;
        }

    }

}
