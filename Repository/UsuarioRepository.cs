using VagasDoc.Data;
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

        public UsuarioModel AlterarSenha(UsuarioModel usuario)
        {
            throw new NotImplementedException();
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login == login);
        }

        public UsuarioModel Criar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Editar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Nome = usuario.Login;
            usuarioDB.Nome = usuario.Senha;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
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
