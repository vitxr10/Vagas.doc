using VagasDoc.Enums;
using System.ComponentModel.DataAnnotations;

namespace VagasDoc.Models
{
    public class VagaModel
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string? Descricao { get; set; }
        public string? Link { get; set; }
        public SituacaoEnum Situacao { get; set; }
        public ModalidadeEnum Modalidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
