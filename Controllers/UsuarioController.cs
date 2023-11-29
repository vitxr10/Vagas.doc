using VagasDoc.Filters;
using VagasDoc.Models;
using VagasDoc.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagasDoc.Data;
using VagasDoc.Session;

namespace VagasDoc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISessao _sessao;
        public UsuarioController(IUsuarioRepository usuarioRepository, ISessao sessao)
        {
            _usuarioRepository = usuarioRepository;
            _sessao = sessao;
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
        public IActionResult AlterarSenha()
        {
            return View();
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

        [FiltroUsuarioLogado]
        [HttpPost]
        public IActionResult AlterarSenha(LoginModel login)
        {
            UsuarioModel usuario = _sessao.BuscarSessaoUsuario();

            if (!(string.Equals(login.Senha, usuario.Senha)))
            {
                TempData["MensagemErro"] = "Senha atual incorreta.";
                return RedirectToAction("AlterarSenha", "Usuario");
            }

            if (!(string.Equals(login.NovaSenha, login.ConfirmacaoNovaSenha)))
            {
                TempData["MensagemErro"] = "A nova senha não coincide com a confirmação.";
                return RedirectToAction("AlterarSenha", "Usuario");
            }

            _usuarioRepository.AlterarSenha(login, usuario);
            return RedirectToAction("Index", "Home");
        }
    }
}
