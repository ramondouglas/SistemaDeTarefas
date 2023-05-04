using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{

    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }
        public async Task<TarefaModel> BuscarPorID(int id)
        {
            return await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTarefas()
        {
            return await _dbContext.Tarefas.ToListAsync();
        }

        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }
        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorID = await BuscarPorID(id);

            if(tarefaPorID == null) 
            {
                throw new Exception($"Tarefa para o ID: {id} Não foi encontrado no Banco de Dados.");
            }
            tarefaPorID.Nome = tarefa.Nome;
            tarefaPorID.Descricao = tarefa.Descricao;
            tarefaPorID.Status = tarefa.Status;
            tarefaPorID.UsuarioID = tarefa.UsuarioID;

            _dbContext.Tarefas.Update(tarefaPorID);
            await _dbContext.SaveChangesAsync();

            return tarefaPorID;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefaPorID = await BuscarPorID(id);

            if (tarefaPorID == null)
            {
                throw new Exception($"Tarefa para o ID: {id} Não foi encontrado no Banco de Dados.");
            }


            _dbContext.Tarefas.Remove(tarefaPorID);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
