using static BACKEND.Models.Tarefa;

namespace BACKEND.DTO
{
    public class TaskListRequestDTO
    {
        public int IdTarefa { get; private set; }
        public string Titulo { get; set; }
        public StatusTarefa Status { get; set; }
        public PrioridadeTarefa Prioridade { get; set; }
    }
}
