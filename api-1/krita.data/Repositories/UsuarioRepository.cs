namespace Krita.Data.Repositories
{
	public class UsuarioRepository : BaseRepository, IUsuarioRepository
	{
		public UsuarioRepository(Connection connection) : base(connection)
		{

		}

        private enum Procedures
        {
            KRTSP_BloquearUsuario,
        }

        public enum Status
        {
            Bloqueado = 0,
        	NaoBloqueado = 1
        }
         public int Desbloquear(int id, int idUsuarioUltimaAlteracao)
        {
            SetProcedure(Procedures.KRTSP_BloquearUsuario);

            AddParameter("id", id);
            AddParameter("usuarioUltimaAlteracao", idUsuarioUltimaAlteracao);
            AddParameter("bloqueado", Status.Bloqueado);

            return ExecuteNonQuery();
        }
    }
}

