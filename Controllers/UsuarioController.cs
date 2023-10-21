using VagasDoc.Filters;
using VagasDoc.Models;
using VagasDoc.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VagasDoc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [FiltroUsuarioLogado]
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepository.ListarPorId(id);
            return View(usuario);
        }

        [FiltroUsuarioLogado]
        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = _usuarioRepository.ListarPorId(id);
            return View(usuario);
        }

        [FiltroUsuarioLogado]
        public IActionResult AlterarSenha(int id)
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
        public IActionResult Editar(UsuarioModel usuario)
        {
            _usuarioRepository.Editar(usuario);
            return RedirectToAction("Index", "Home");
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
