namespace Krita.Shared.Dto
{
    public class BuscarUsuarioDto
    {
        public int? Id { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public int? Bloqueado { get; set; }
    }
}
