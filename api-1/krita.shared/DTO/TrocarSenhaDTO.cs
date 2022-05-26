namespace Krita.Shared.Dto
{
    public class TrocarSenhaDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
    }
}
