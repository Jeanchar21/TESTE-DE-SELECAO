using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BACKEND.Models
{
    public class Tarefa
    {
        [Key]
        public int IdTarefa { get; private set; }
        public string Titulo { get; set; }
        public string Situacao { get; set; }
        public StatusTarefa Status { get; set; }
        public PrioridadeTarefa Prioridade { get; set; }

        public Tarefa(int idTarefa, string situacao, StatusTarefa status, PrioridadeTarefa prioridade)
        {
            IdTarefa = idTarefa;
            Situacao = situacao;
            Status = status;
            Prioridade = prioridade;
        }
        public Tarefa (string titulo, string situacao, StatusTarefa status, PrioridadeTarefa prioridade)
        {
            Titulo = titulo;
            Situacao = situacao;
            Status = status;
            Prioridade = prioridade;
        }

        public Tarefa() { }

        public enum StatusTarefa
        {
            pendente,
            concluido
        }

        public enum PrioridadeTarefa
        {
            baixa,
            media,
            alta
        }
    }
}