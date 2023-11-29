using Microsoft.AspNetCore.Mvc;
using VagasDoc.Helper;
using VagasDoc.Models;
using VagasDoc.Repository;

namespace VagasDoc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuariosRepository;
        private readonly ISessao _sessao;
        public LoginController(IUsuarioRepository usuarioRepository, ISessao sessao)
        {
            _usuariosRepository = usuarioRepository;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            //if (_sessao.BuscarSessaoUsuario != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                UsuarioModel usuario = _usuariosRepository.BuscarPorLogin(loginModel.Login);
                if (usuario != null)
                {
                    var senha = loginModel.Senha;
                    senha = Cripto.Encrypt(senha);

                    if (usuario.SenhaValida(senha))
                    {
                        _sessao.CriarSessaoUsuario(usuario);
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["MensagemErro"] = "Senha inválida, tente novamente.";
                }
                else
                {
                    TempData["MensagemErro"] = "Email e/ou senha inválido(s), tente novamente.";
                }

                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Não foi possível fazer o login {e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return View("Index");
        }
    }
}
