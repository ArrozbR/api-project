using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase{
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context){
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id){
            var tarefabanco = _context.Tarefas.Find(id);
            if(tarefabanco == null) return NotFound();
            return Ok(tarefabanco);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos(){
            if(!_context.Tarefas.Any()) return NotFound();
            return Ok(_context.Tarefas);
        }

        [HttpGet("ObterPorTitulo/{titulo}")]
        public IActionResult ObterPorTitulo(string titulo){
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            if(!tarefa.Any()) return NotFound();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData/{data}")]
        public IActionResult ObterPorData(DateTime data){
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            if(!tarefa.Any()) return NotFound();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus/{status}")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status){
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            if(!tarefa.Any()) return NotFound();
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa){
            if (tarefa.Data == DateTime.MinValue) return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa){
            var tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null) return NotFound();
            if (tarefa.Data == DateTime.MinValue) return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id){
            var tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null) return NotFound();
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
