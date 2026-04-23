using BACKEND.Data;
using BACKEND.DTO;
using BACKEND.Interface;
using BACKEND.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> GetTasks()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa> GetTask(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return null;
            }

            return tarefa;
        }

        
        public async Task PutTask(int id, TaskUpdateStatusDTO tarefa)
        {
            var alterTask = await _context.Tarefas.FindAsync(id);
            alterTask.Status = tarefa.Status;
            _context.Entry(alterTask).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }

            return;
        }

        
        public async Task<Tarefa> PostTask(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return tarefa;
        }

        public async Task DeleteTask(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }

            return;
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.IdTarefa == id);
        }
    }
}
