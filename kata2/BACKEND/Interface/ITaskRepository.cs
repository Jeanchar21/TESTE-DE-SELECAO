using BACKEND.DTO;
using BACKEND.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tarefa>> GetTasks();
        Task<Tarefa> GetTask(int id);
        Task PutTask(int id, TaskUpdateStatusDTO tarefa);
        Task<Tarefa> PostTask(Tarefa tarefa);
        Task DeleteTask(int id);
    }
}
