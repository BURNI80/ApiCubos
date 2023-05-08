using ApiCubos.Data;
using ApiCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubos.Repositories
{
    public class CubosRepository
    {

        private CubosContext context;

        public CubosRepository(CubosContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> ExisteUsuarioAsync(string email, string pass)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Pass == pass);
        }


        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<List<Cubo>> FindCubo(string marca)
        {
            return await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        }


        public async Task CreateUser(Usuario user)
        {
            user.Id = await this.context.Usuarios.MaxAsync(x => x.Id) + 1;
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateCubo(Cubo cubo)
        {
            cubo.Id = await this.context.Cubos.MaxAsync(x => x.Id) + 1;
            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
        }

        //public async Task<Usuario> GetPerfilUsuario(int id)
        //{
        //    return await this.context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
        //}

        public async Task<List<Pedido>> GetPedidosUsuario(int id)
        {
            return await this.context.Pedidos.Where(x => x.IdUsuario == id).ToListAsync();
        }

        public async Task CreatePedido(int id, int iduser)
        {
            Pedido pedido = new Pedido
            {
                IdPedido = await this.context.Pedidos.MaxAsync(x => x.IdPedido) + 1,
                IdCubo = id,
                Fecha = DateTime.Now,
                IdUsuario = iduser
            };
            pedido.IdPedido = await this.context.Pedidos.MaxAsync(x => x.IdPedido) + 1;
            this.context.Pedidos.Add(pedido);
            await this.context.SaveChangesAsync();
        }
    }
}
