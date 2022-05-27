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
        public IActionResult login([FromBody] LoginDto login)
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
            return Ok(new {
                token = token
            });
        }

        [HttpPost("esqueci-senha")]
        public IActionResult EsqueciSenha([FromBody] EsqueciSenhaDto novoUsuario)
        {
            Console.WriteLine(novoUsuario.Email);
            Console.WriteLine(novoUsuario.Senha);
            novoUsuario.Senha = novoUsuario.Senha.Criptografar();

            var resultadoAlteracao = _usuarioRepository.AlterarSenha(novoUsuario);
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
