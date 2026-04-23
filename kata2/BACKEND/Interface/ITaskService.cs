using BACKEND.DTO;
using BACKEND.Models;
using NuGet.Protocol.Core.Types;
using static BACKEND.Models.Tarefa;

namespace BACKEND.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskListRequestDTO>> Get(StatusTarefa? status, int pageNumber, int pageSize);
        Task<Tarefa> GetId(int id);
        Task PutTarefa(int id, TaskUpdateStatusDTO tarefa);
        Task<Tarefa> Post(Tarefa tarefa);
        Task Delete(int id);
    }
}
