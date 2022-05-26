namespace Krita.Shared.Interfaces
{
    public interface IUsuarioRepository
    {
        int Cadastrar(CadastroDto cadastroDto);
        int AlterarSenha(EsqueciSenhaDto esqueciSenha);
        int Bloquear(int id, int idUsuarioUltimaAlteracao);
        ICollection<Usuario> Buscar(BuscarUsuarioDto dto);
        int Editar(int id, EditarUsuarioDto editarUsuarioDto);
        int Desbloquear(int id, int idUsuarioUltimaAlteracao);
        int TrocarSenha(TrocarSenhaDto trocarSenha);
        Usuario BuscarPorId(int id);
        Usuario BuscarPorEmail(string email);
    }
}
