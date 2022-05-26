namespace krita.Shared.Models
{
    public class ColaboradorEntrega
    {
         public short Id { get; set; }
        public short IdColaborador { get; set; }
        public int IdEntrega { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeMinutosCobrados { get; set; }
        public int QuantidadeMinutosNaoCobrados { get; set; }
        public int UsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? UsuarioUltimaAlteracao { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public virtual Colaborador Colaborador { get; set; }
        public virtual Entrega Entregas {get; set; }
    }
}