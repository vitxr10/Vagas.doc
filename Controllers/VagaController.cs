using VagasDoc.Filters;
using VagasDoc.Models;
using VagasDoc.Repository;
using VagasDoc.Helper;
using Microsoft.AspNetCore.Mvc;

namespace VagasDoc.Controllers
{
    [FiltroUsuarioLogado]
    public class VagaController : Controller
    {
        private readonly IVagaRepository _vagaRepository;
        private readonly ISessao _sessao;
        public VagaController(IVagaRepository vagaRepository, ISessao sessao)
        {
            _vagaRepository = vagaRepository;
            _sessao = sessao;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();

            var listaVagas = _vagaRepository.Listar(usuarioLogado.Id);
            return View(listaVagas);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            VagaModel vaga = _vagaRepository.ListarPorId(id);
            return View(vaga);
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            VagaModel vaga = _vagaRepository.ListarPorId(id);
            return View(vaga);
        }

        [HttpPost]
        public IActionResult Criar(VagaModel vaga)
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            vaga.UsuarioId = usuarioLogado.Id;

            _vagaRepository.Criar(vaga);
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public IActionResult Editar(VagaModel vaga)
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            vaga.UsuarioId = usuarioLogado.Id;

            _vagaRepository.Editar(vaga);
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public IActionResult Excluir(VagaModel vaga)
        {
            _vagaRepository.Excluir(vaga);
            return RedirectToAction("Listar");
        }

    }
}