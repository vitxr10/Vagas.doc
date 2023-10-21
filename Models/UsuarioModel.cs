namespace VagasDoc.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        //lista de vagas que cada usuario tem
        public virtual List<VagaModel> Vagas { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
