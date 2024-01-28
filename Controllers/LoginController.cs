using Azure;
using Microsoft.AspNetCore.Mvc;
using VagasDoc.Data;
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
        private readonly IEmail _email;
        private readonly BancoContext _bancoContext;
        public LoginController(IUsuarioRepository usuarioRepository, ISessao sessao, IConfiguration configuration, IEmail email, BancoContext bancoContext)
        {
            _usuariosRepository = usuarioRepository;
            _sessao = sessao;
            _configuration = configuration;
            _email = email;
            _bancoContext = bancoContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //if (_sessao.BuscarSessaoUsuario != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        public IActionResult EsqueciMinhaSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntrarAsync(IFormCollection form)
        {
            var token = form["cf-turnstile-response"];

            if (string.IsNullOrEmpty(token) || !await ValidaCaptchaAsync(token))
            {
                TempData["MensagemErro"] = "Captcha inválido.";
                return RedirectToAction("Index");
            }

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

                TempData["MensagemErro"] = "Senha inválida.";
            }
            else
            {
                TempData["MensagemErro"] = "Email e/ou senha inválido(s).";
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<bool> ValidaCaptchaAsync(string token)
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
            var captchaResult = await request.Content.ReadFromJsonAsync<CaptchaModel>();

            if (captchaResult.Success)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public IActionResult EsqueciMinhaSenha(string login)
        {
            UsuarioModel usuario = _usuariosRepository.BuscarPorLogin(login);

            if (usuario == null)
            {
                TempData["MensagemErro"] = "Este login não existe.";
                return RedirectToAction("EsqueciMinhaSenha");
            }

            Random random = new Random();

            var novaSenha = random.Next();
            string mensagem = $"<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><title>Redefinição de Senha</title><style>body {{font-family: Arial, sans-serif;background-color: #f0f5f9;margin: 0;padding: 0;}}.container {{width: 100%;max-width: 600px;margin: 20px auto;padding: 20px;background-color: #fff;border-radius: 8px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);border: 4px solid #2a75b3;}}h1 {{color: #2a75b3;text-align: center;}}.message {{text-align: center;margin-top: 20px;color: #333;font-size: 18px;}}.message strong {{font-weight: bold;color: #2a75b3;}}</style></head><body><div class=\"container\"><h1>Redefinição de Senha</h1><div class=\"message\"><p>Sua nova senha é <strong>{novaSenha}</strong></p></div></div></body></html>";
            string assunto = "Redefinição de senha";

            if (_email.Enviar(usuario.Login, assunto, mensagem))
            {
                usuario.Senha = Cripto.Encrypt(Convert.ToString(novaSenha));

                _bancoContext.Usuarios.Update(usuario);
                _bancoContext.SaveChanges();

                TempData["MensagemSucesso"] = "Email enviado.";
                return RedirectToAction("EsqueciMinhaSenha");
            }
            else
            {
                TempData["MensagemErro"] = "Não foi possível redefinir sua senha.";
                return RedirectToAction("EsqueciMinhaSenha");
            }
        }

        [HttpGet]
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return View("Index");
        }
    }
}
