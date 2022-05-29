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

		[HttpPut("{id}/desbloquear")]
        public IActionResult Desbloquear(int id, [FromBody] int idUsuarioUltimaAlteracao)
        {
            var linhasAlteradas = _usuarioRepository.Desbloquear(id, idUsuarioUltimaAlteracao);
            if (linhasAlteradas == 0)
                return BadRequest("Não foi possível desbloquear o usuário");
            return Ok();
        }
    }
}
