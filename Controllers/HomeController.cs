using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VagasDoc.Filters;
using VagasDoc.Helper;
using VagasDoc.Models;

namespace VagasDoc.Controllers
{
    [FiltroUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessao _sessao;

        public HomeController(ILogger<HomeController> logger, ISessao sessao)
        {
            _logger = logger;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuario = _sessao.BuscarSessaoUsuario();
            return View(usuario);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}