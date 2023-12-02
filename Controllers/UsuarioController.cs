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
        public IActionResult Excluir(UsuarioModel usuario)
        {
            _usuarioRepository.Excluir(usuario);
            return RedirectToAction("Index", "Login");
        }

        [FiltroUsuarioLogado]
        [HttpPost]
        public IActionResult AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuario = _sessao.BuscarSessaoUsuario();

            var senhaAtual = alterarSenhaModel.SenhaAtual;
            senhaAtual = Cripto.Encrypt(senhaAtual);

            if (!(string.Equals(senhaAtual, usuario.Senha)))
            {
                TempData["MensagemErro"] = "Senha atual incorreta.";
                return RedirectToAction("AlterarSenha", "Usuario");
            }

            if (!(string.Equals(alterarSenhaModel.NovaSenha, alterarSenhaModel.ConfirmacaoNovaSenha)))
            {
                TempData["MensagemErro"] = "A senha nova não coincide com a confirmação.";
                return RedirectToAction("AlterarSenha", "Usuario");
            }

            _usuarioRepository.AlterarSenha(alterarSenhaModel, usuario);
            return RedirectToAction("Index", "Home");
        }
    }
}
