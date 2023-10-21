using VagasDoc.Models;

namespace VagasDoc.Repository
{
    public interface IVagaRepository
    {
        VagaModel Criar(VagaModel vaga);

        List<VagaModel> Listar(int usuarioId);

        VagaModel ListarPorId(int id);

        VagaModel Editar(VagaModel vaga);

        void Excluir(VagaModel vaga);
    }
}
