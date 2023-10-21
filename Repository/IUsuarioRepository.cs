using VagasDoc.Models;

namespace VagasDoc.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel Criar(UsuarioModel usuario);
        UsuarioModel Editar(UsuarioModel usuario);
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> Listar();
        void Excluir(UsuarioModel usuario);
        UsuarioModel AlterarSenha(UsuarioModel usuario);
    }
}
