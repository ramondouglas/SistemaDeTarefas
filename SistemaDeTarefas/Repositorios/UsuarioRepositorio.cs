using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{

    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }
        public async Task<UsuarioModel> BuscarPorID(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorID = await BuscarPorID(id);

            if (usuarioPorID == null) 
            {
                throw new Exception($"Usuario para o ID: {id} Não foi encontrado no Banco de Dados.");
            }
            usuarioPorID.Nome = usuario.Nome;
            usuarioPorID.Email = usuario.Email;

            _dbContext.Usuarios.Update(usuarioPorID);
            await _dbContext.SaveChangesAsync();

            return usuarioPorID;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorID = await BuscarPorID(id);

            if (usuarioPorID == null)
            {
                throw new Exception($"Usuario para o ID: {id} Não foi encontrado no Banco de Dados.");
            }


            _dbContext.Usuarios.Remove(usuarioPorID);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
