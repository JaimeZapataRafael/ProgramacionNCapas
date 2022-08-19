using Microsoft.AspNetCore.Mvc;

namespace SL_WebAPI.Controllers
{

    [ApiController]
    public class UsuarioAdd : Controller
    {

        [Route("/api/Usuario/Add")]
        [HttpPost]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("/api/Usuario/Update")]
        [HttpPost]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("/api/Usuario/Delete")]
        [HttpPost]
        public IActionResult Delete([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Delete(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("/api/Usuario/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            var result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("/api/Usuario/GetById")]
        [HttpGet]
        public IActionResult GetById(int IdUsuario)
        {
            var result = BL.Usuario.GetById(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


    }
}

