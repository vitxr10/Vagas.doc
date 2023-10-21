using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VagasDoc.Models;

namespace VagasDoc.ViewComponents
{
    public class Perfil : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

            return View(usuario);
        }
    }
}
