using VagasDoc.Data;
using VagasDoc.Models;

namespace VagasDoc.Repository
{
    public class VagaRepository : IVagaRepository
    {
        private readonly BancoContext _bancoContext;

        public VagaRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public VagaModel Criar(VagaModel vaga)
        {
            vaga.DataCadastro = DateTime.Now;

            _bancoContext.Vagas.Add(vaga);
            _bancoContext.SaveChanges();

            return vaga;
        }

        public List<VagaModel> Listar(int usuarioId)
        {
            return _bancoContext.Vagas.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        public VagaModel ListarPorId(int id)
        {
            return _bancoContext.Vagas.FirstOrDefault(x => x.Id == id);

        }

        public VagaModel Editar(VagaModel vaga)
        {
            VagaModel vagaDB = ListarPorId(vaga.Id);

            if (vagaDB == null)
            {
                throw new Exception("Erro: esta vaga não existe");
            }

            vagaDB.Titulo = vaga.Titulo;
            vagaDB.Empresa = vaga.Empresa;
            vagaDB.Descricao = vaga.Descricao;
            vagaDB.Link = vaga.Link;
            vagaDB.Situacao = vaga.Situacao;
            vagaDB.Modalidade = vaga.Modalidade;
            vagaDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Vagas.Update(vagaDB);
            _bancoContext.SaveChanges();

            return vaga;
        }

        public void Excluir(VagaModel vaga)
        {
            VagaModel vagaDB = ListarPorId(vaga.Id);

            _bancoContext.Vagas.Remove(vagaDB);
            _bancoContext.SaveChanges();
        }
    }
}
