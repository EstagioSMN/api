namespace krita.Shared.Models
{
    public class Entrega
    {
         public int Id { get; set; }
        public int IdProjeto { get; set; }
        public short IdContatoHomologacao { get; set; }
        public short IdContatoAprovacao { get; set; }
        public DateTime? DataInicioPrevista { get; set; }
        public DateTime? DataTerminoPrevista { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string Descricao { get; set; }
        public int UsuarioCadastro { get; set; }
        public int DataCadastro { get; set; }
        public int? UsuarioUltimaAlteracao { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public virtual Entrega Entregas { get; set; }
        public virtual ICollection<ColaboradorEntrega> ColaboradorEntregas {get; set; }
    }
}