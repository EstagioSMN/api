namespace Krita.Shared.Interfaces
{
    public interface IUsuarioRepository
    {
        int Desbloquear(int id, int idUsuarioUltimaAlteracao);
    }
}
