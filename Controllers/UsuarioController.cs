using VagasDoc.Filters;
using VagasDoc.Models;
using VagasDoc.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagasDoc.Data;
using VagasDoc.Helper;

namespace VagasDoc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [FiltroUsuarioLogado]
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = _usuarioRepository.ListarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            _usuarioRepository.Criar(usuario);
            return RedirectToAction("Index", "Login");
        }

        [FiltroUsuarioLogado]
        [HttpPost]
        public IActionResult Excluir(UsuarioModel usuario)
        {
            _usuarioRepository.Excluir(usuario);
            return RedirectToAction("Index", "Login");
        }

        
    }
}
