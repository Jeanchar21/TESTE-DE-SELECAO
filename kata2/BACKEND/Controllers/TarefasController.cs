using BACKEND.Data;
using BACKEND.DTO;
using BACKEND.Interface;
using BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BACKEND.Models.Tarefa;

namespace BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/Tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListRequestDTO>>> GetTarefas(StatusTarefa? status, int pageNumber = 1,
            int pageSize = 12)
        {
            var result = await _service.Get(status, pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Tarefas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _service.GetId(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada");
            }

            return Ok(tarefa);
        }

        // PUT: api/Tarefas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, TaskUpdateStatusDTO tarefa)
        {
            var tarefaEncontrada = await _service.GetId(id);

            if(tarefaEncontrada == null)
            {
                BadRequest("Tarefa não encontrada para atualizar");
            }

            try
            {
                await _service.PutTarefa(id, tarefa);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (tarefaEncontrada == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Tarefa concluída com sucesso!");
        }

        // POST: api/Tarefas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            var PostedTask = await _service.Post(tarefa);

            return CreatedAtAction("GetTarefa", new { id = PostedTask.IdTarefa }, PostedTask);
        }

        // DELETE: api/Tarefas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _service.GetId(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return Ok("Tarefa deletada com sucesso!");
        }
    }
}
