namespace Krita.Controllers
{
    [Route("usuario")]
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public IActionResult login(LoginDto login)
        {
            var usuario = _usuarioRepository.BuscarPorEmail(login.Email);

            if (usuario == null)
                return BadRequest("Email ou senha incorreta!");

            if (!usuario.Senha.Equals(login.Senha.Criptografar()))
                return BadRequest("Login ou senha incorretos!");

            if (usuario.DataBloqueio != null)
                return BadRequest("Usuário bloqueado!");

            if (usuario.DataExpiracaoSenha <= DateTime.Now.Date)
            {
                return BadRequest("Sua senha expirou");
            }

            var token = TokenService.GenerateToken(usuario);
            return Ok(token);
        }

        [HttpPost("{id}/esqueci-senha")]
        public IActionResult EsqueciSenha(int id, [FromBody] EsqueciSenhaDto novoUsuario)
        {
            var usuario = new EsqueciSenhaDto()
            {
                Id = id,
                Senha = novoUsuario.Senha.Criptografar()
            };

            var resultadoAlteracao = _usuarioRepository.AlterarSenha(usuario);
            if (resultadoAlteracao == 0)
                return BadRequest("Erro ao atualizar Senha");

            return Ok();
        }
        
        [HttpPost("trocar-senha")]
        public IActionResult TrocarSenha([FromBody] TrocarSenhaDto trocarSenhaDto)
        {
            var usuario = _usuarioRepository.BuscarPorEmail(trocarSenhaDto.Email);

            if (usuario == null) {
                return BadRequest("Email não existe");
            }

            if (!usuario.Senha.Equals(trocarSenhaDto.SenhaAtual.Criptografar()))
                return BadRequest("Senha incorreta");

            if (usuario.DataBloqueio != null)
                return BadRequest("Usuário bloqueado");

            trocarSenhaDto.NovaSenha = trocarSenhaDto.NovaSenha.Criptografar();

            var qtdLinhasAlteradas = _usuarioRepository.TrocarSenha(trocarSenhaDto);

            if (qtdLinhasAlteradas == 0)
                return BadRequest("Senha não alterada");

            return Ok();
        }
    }
}
