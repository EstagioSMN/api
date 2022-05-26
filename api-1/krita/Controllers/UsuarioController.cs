namespace Krita.Controllers
{
    [Route("usuario")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController
        (
            IUsuarioRepository usuarioRepository
        )
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("buscar")]
        public IActionResult Buscar([FromBody] BuscarUsuarioDto dto)
        {
            var usuarios = _usuarioRepository.Buscar(dto);
            if (usuarios == null)
                return BadRequest("Usuarios não encontrados");
            return Ok(usuarios);
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] CadastroDto cadastroDto)
        {
            cadastroDto.Senha = cadastroDto.Senha.Criptografar();

            if (string.IsNullOrEmpty(cadastroDto.Apelido))
                return BadRequest("Apelido invalido");

            if (string.IsNullOrEmpty(cadastroDto.Email))
                return BadRequest("Email invalido");

            var cadastro = _usuarioRepository.Cadastrar(cadastroDto);

            if (cadastro == 0)
                return BadRequest("Usuario não cadastradao");

            return Ok(cadastroDto);
        }

        [HttpGet("VerificarEmail")]
        public IActionResult VerificarEmail([FromQuery] string email)
        {
            var usuarios = _usuarioRepository.BuscarPorEmail(email);
            if (usuarios == null) 
				return BadRequest("Email não encontrado.");
            return Ok(usuarios);
        }

		[HttpPut("{id}/bloquear")]
		public IActionResult Bloquear(int id, [FromBody] int idUsuarioUltimaAlteracao)
		{
			var linhasAlteradas = _usuarioRepository.Bloquear(id, idUsuarioUltimaAlteracao);
			if (linhasAlteradas == 0)
				return BadRequest("Não foi possível bloquear o usuário");

			return Ok();
		}

		[HttpPut("{id}/desbloquear")]
        public IActionResult Desbloquear(int id, [FromBody] int idUsuarioUltimaAlteracao)
        {
            var linhasAlteradas = _usuarioRepository.Desbloquear(id, idUsuarioUltimaAlteracao);
            if (linhasAlteradas == 0)
                return BadRequest("Não foi possível desbloquear o usuário");
            return Ok();
        }
		
		[HttpPut("{id}/editar")]
		public IActionResult Editar(int id, [FromBody] EditarUsuarioDto editarUsuarioDto) 
		{
			var usuario = _usuarioRepository.BuscarPorId(id);

			if(usuario == null)
				BadRequest("Usuário não encontrado");

			return Ok(_usuarioRepository.Editar(id, editarUsuarioDto));
		}
    }
}
