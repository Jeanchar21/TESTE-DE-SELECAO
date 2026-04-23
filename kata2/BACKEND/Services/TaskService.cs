using AutoMapper;
using BACKEND.Data;
using BACKEND.DTO;
using BACKEND.Interface;
using BACKEND.Models;
using static BACKEND.Models.Tarefa;

namespace BACKEND.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskListRequestDTO>> Get(StatusTarefa? status, int pageNumber,
            int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            IEnumerable<Tarefa> tasks = await _repository.GetTasks();
            if (status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == status);
            }
            IEnumerable<TaskListRequestDTO> tasksDto = _mapper.Map<IEnumerable<TaskListRequestDTO>>(tasks.Skip(skip).Take(pageSize));
            return tasksDto;
        }

        public async Task<Tarefa> GetId(int id)
        {
            var tarefa = await _repository.GetTask(id);

            return tarefa;
        }

        public async Task PutTarefa(int id, TaskUpdateStatusDTO tarefa)
        {
            await _repository.PutTask(id, tarefa);
            return;
        }

        public async Task<Tarefa> Post(Tarefa tarefa)
        {
            await _repository.PostTask(tarefa);
            return tarefa;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteTask(id);

            return;
        }
    }
}
