namespace Krita.Shared.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime? DataBloqueio { get; set; }
        public int UsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? UsuarioUltimaAlteracao { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}
