using ApiCubos.Models;
using ApiCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {

        private CubosRepository repo;
        
        public CubosController(CubosRepository repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        [Route("/api/[action]")]
        public async Task<ActionResult<List<Cubo>>> GetCubosAsync()
        {
            return await this.repo.GetCubosAsync();
        }

        [HttpGet]
        [Route("/api/[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>> FindCuboMarca(string marca)
        {
            return await this.repo.FindCubo(marca);
        }

        [HttpPost]
        [Route("/api/[action]")]
        public async Task CreateUsuario(Usuario user)
        {
            await this.repo.CreateUser(user);
        }

        [HttpPost]
        [Route("/api/[action]")]
        public async Task CreateCubo(Cubo cubo)
        {
            await this.repo.CreateCubo(cubo);
        }


        [Authorize]
        [HttpGet]
        [Route("/api/[action]")]
        public async Task<ActionResult<Usuario>> PerfilUsuario()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return usuario;
        }

        [Authorize]
        [HttpGet]
        [Route("/api/[action]")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosUsuario()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return await this.repo.GetPedidosUsuario(usuario.Id);
        }

        [Authorize]
        [HttpPost]
        [Route("/api/[action]/{id}")]
        public async Task CreatePedido(int id)
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            await this.repo.CreatePedido(id, usuario.Id );
        }




    }
}
