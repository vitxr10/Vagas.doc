using Microsoft.AspNetCore.Mvc;
using VagasDoc.Data;
using VagasDoc.Filters;
using VagasDoc.Helper;
using VagasDoc.Models;
using VagasDoc.Repository;

namespace VagasDoc.Controllers
{
    [FiltroUsuarioLogado]
    public class AlterarSenhaController : Controller
    {
        private readonly ISessao _sessao;
        private readonly BancoContext _bancoContext;

        public AlterarSenhaController(ISessao sessao, BancoContext bancoContext)
        {
            _sessao = sessao;
            _bancoContext = bancoContext;
        }

        [HttpGet]
        public IActionResult AlterarSenha()
        {
            return View();
        }

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

            var novaSenha = Cripto.Encrypt(alterarSenhaModel.NovaSenha);
            usuario.Senha = novaSenha;
            usuario.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuario);
            _bancoContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
