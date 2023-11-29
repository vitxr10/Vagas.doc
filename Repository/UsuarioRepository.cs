using VagasDoc.Data;
using VagasDoc.Helper;
using VagasDoc.Models;

namespace VagasDoc.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public void AlterarSenha(AlterarSenhaModel alterarSenhaModel, UsuarioModel usuario)
        {
            var novaSenha = alterarSenhaModel.NovaSenha;
            usuario.Senha = Cripto.Encrypt(novaSenha);

            _bancoContext.Usuarios.Update(usuario);
            _bancoContext.SaveChanges();
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login == login);
        }

        public UsuarioModel Criar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.Senha = Cripto.Encrypt(usuario.Senha);
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public void Excluir(UsuarioModel usuario)
        {
            var listaVagas = _bancoContext.Vagas.Where(x => x.UsuarioId == usuario.Id).ToList();

            foreach (var vaga in listaVagas)
            {
                _bancoContext.Vagas.Remove(vaga);
                _bancoContext.SaveChanges();
            }

            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();
        }

        public List<UsuarioModel> Listar()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }
    }
}
