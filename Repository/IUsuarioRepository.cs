using VagasDoc.Models;

namespace VagasDoc.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel Criar(UsuarioModel usuario);
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> Listar();
        void Excluir(UsuarioModel usuario);
        void AlterarSenha(AlterarSenhaModel alterarSenhaModel, UsuarioModel usuario);
    }
}
