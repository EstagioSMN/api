namespace Krita.Data.Repositories
{
	public class UsuarioRepository : BaseRepository, IUsuarioRepository
	{
		public UsuarioRepository(Connection connection) : base(connection)
		{

		}

        private enum Procedures
        {
            KRTSP_InsUsuario,
            KRTSP_SelUsuario,
            KRTSP_UpdUsuario,
            KRTSP_BloquearUsuario,
            KRTSP_TrocarSenha
        }

        public enum Status
        {
            Bloqueado = 0,
        	NaoBloqueado = 1
        }

        public int Cadastrar(CadastroDto cadastroDto)
        {
            SetProcedure(Procedures.KRTSP_InsUsuario);

            AddParameter("apelido", cadastroDto.Apelido);
            AddParameter("email", cadastroDto.Email);
            AddParameter("senha", cadastroDto.Senha);
            AddParameter("usuarioCadastro", cadastroDto.IdUsuarioCadastro);

			return ExecuteNonQuery();
		}
        
        public Usuario BuscarPorEmail(string email)
        {
            SetProcedure(Procedures.KRTSP_SelUsuario);

            var usuarios = new List<Usuario>();

            var reader = ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new Usuario()
                {
                    Id = (int)reader["id"],
                    Apelido = (string)reader["apelido"],
                    Email = (string)reader["email"],
                    Senha = (string)reader["senha"],
                    DataExpiracaoSenha = (DateTime)reader["dataExpiracaoSenha"]
                });
            }
            Close();
            return usuarios.Where(x => x.Email.Equals(email)).FirstOrDefault();
        }
		
        public ICollection<Usuario> Buscar(BuscarUsuarioDto dto)
        {
            SetProcedure(Procedures.KRTSP_SelUsuario);

            var usuarios = new List<Usuario>();

            if (dto.Id != null)
                AddParameter("id", dto.Id);

            if (!string.IsNullOrEmpty(dto.Apelido))
                AddParameter("apelido", dto.Apelido);

            if (!string.IsNullOrEmpty(dto.Email))
                AddParameter("email", dto.Email);

            if (dto.Bloqueado.HasValue && dto.Bloqueado.Value == (int)Status.Bloqueado)
                AddParameter("Bloqueado", Status.Bloqueado);

            if (dto.Bloqueado.HasValue && dto.Bloqueado.Value == (int)Status.NaoBloqueado)
                AddParameter("Bloqueado", Status.NaoBloqueado);

            var reader = ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new Usuario()
                {
                    Id = (int)reader["id"],
                    Apelido = (string)reader["apelido"],
                    Email = (string)reader["email"],
                    DataBloqueio = !string.IsNullOrEmpty(reader["dataBloqueio"].ToString()) ? (DateTime)reader["dataBloqueio"] : null
                });
            }

            return usuarios;
        }

		public int AlterarSenha(EsqueciSenhaDto esqueciSenha)
		{
			SetProcedure(Procedures.KRTSP_TrocarSenha);

			AddParameter("email", esqueciSenha.Email);
			AddParameter("novaSenha", esqueciSenha.Senha);

			return ExecuteNonQuery();
		}
		
		public int TrocarSenha(TrocarSenhaDto trocarSenha)
		{
			SetProcedure(Procedures.KRTSP_TrocarSenha);

			AddParameter("email", trocarSenha.Email);
			AddParameter("novaSenha", trocarSenha.NovaSenha);

			return ExecuteNonQuery();
		}

		public Usuario BuscarPorId(int id)
		{
			SetProcedure(Procedures.KRTSP_SelUsuario);

			AddParameter("id", id);

			var reader = ExecuteReader();
			var usuarios = new List<Usuario>();

			while (reader.Read())
			{
				usuarios.Add(new Usuario()
				{
					Email = (string)reader["email"],
					Apelido = (string)reader["apelido"],
					Senha = (string)reader["senha"],
					DataExpiracaoSenha = (DateTime)reader["dataExpiracaoSenha"],
					DataBloqueio = !String.IsNullOrEmpty(reader["dataBloqueio"].ToString()) ? (DateTime)reader["dataBloqueio"] : null
				});
			}
			Close();
			return usuarios.FirstOrDefault();
		}
		
		public int Editar(int id, EditarUsuarioDto editarUsuarioDto)
		{
			SetProcedure(Procedures.KRTSP_UpdUsuario);

			AddParameter("id", id);
			AddParameter("apelido", editarUsuarioDto.Apelido);
			AddParameter("usuarioUltimaAlteracao", editarUsuarioDto.UsuarioUltimaAlteracao);
            
			return ExecuteNonQuery();
		}
	
        public int Bloquear(int id, int idUsuarioUltimaAlteracao)
        {
            SetProcedure(Procedures.KRTSP_BloquearUsuario);

            AddParameter("id", id);
            AddParameter("usuarioUltimaAlteracao", idUsuarioUltimaAlteracao);
            AddParameter("bloqueado", Status.NaoBloqueado);

            return ExecuteNonQuery();
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

