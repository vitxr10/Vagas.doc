using Azure;
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
        private readonly IConfiguration _configuration;
        public LoginController(IUsuarioRepository usuarioRepository, ISessao sessao, IConfiguration configuration)
        {
            _usuariosRepository = usuarioRepository;
            _sessao = sessao;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            //if (_sessao.BuscarSessaoUsuario != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntrarAsync(IFormCollection form)
        {
            var token = form["cf-turnstile-response"];

            if (!string.IsNullOrEmpty(token) )
            {
                var tokenValido = await ValidaCaptcha(token);
                if (!tokenValido)
                {
                    TempData["MensagemErro"] = "Captcha inválido.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MensagemErro"] = "Captcha inválido.";
                return RedirectToAction("Index");
            }

            try
            {
                var login = form["Login"];
                var senha = form["Senha"];

                UsuarioModel usuario = _usuariosRepository.BuscarPorLogin(login);
                if (usuario != null)
                {
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

        public async Task<bool> ValidaCaptcha(string token)
        {
            var url = "https://challenges.cloudflare.com/turnstile/v0/siteverify";

            var secret = _configuration["Keys:Turnstile.Captcha.Secret"];

            var formValues = new Dictionary<string, string>
            {
                ["secret"] = secret,
                ["response"] = token
            };

            var formData = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(formValues)
            };

            HttpClient httpClient = new HttpClient();

            var request = await httpClient.SendAsync(formData);
            var retornoCaptcha = await request.Content.ReadFromJsonAsync<CaptchaModel>();

            if (retornoCaptcha.Success)
            {
                return true;
            }

            return false;
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return View("Index");
        }
    }
}
