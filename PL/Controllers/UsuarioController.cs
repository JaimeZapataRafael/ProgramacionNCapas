using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]// GET: Usuario / action verb
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();


            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error" + result.ErrorMessage;
            }
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            if (IdUsuario == null)
            {
                IdUsuario = 0;
            }

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();

            if (resultRol.Correct && resultPais.Correct)
            {
                if (IdUsuario.Value == 0)
                {
                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    return View(usuario);
                }
                else
                {
                    ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                    if (result.Correct)
                    {

                        usuario = (ML.Usuario)result.Object;
                        ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                        ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        usuario.Rol.Roles = resultRol.Objects;
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                        return View(usuario);
                    }
                    else
                    {

                        return View("Modal");
                    }
                }

            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al realizar la consulta" + resultRol.ErrorMessage;
                return View("Modal");
            }


        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile imagen = Request.Form.Files["fuImage"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                usuario.Imagen = Convert.ToBase64String(ImagenByte);

            }

            if (usuario.IdUsuario == 0)
            {
                ML.Result result = BL.Usuario.Add(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro Exitoso";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un Error" + result.ErrorMessage;
                }
                return View("Modal");
            }
            else
            {
                ML.Result result = BL.Usuario.Update(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro Actualizado";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un Error" + result.ErrorMessage;
                }
                return View("Modal");
            }
        }
        [HttpGet]
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Delete(usuario);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un Error" + result.ErrorMessage;
            }
            return View("Modal");
        }
        //jsons
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.EstadoGetByIdPais(IdPais);
            return Json(result.Objects); //JsonRequestBehavior.AllowGet);

            //return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result.Objects);
            //return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result.Objects);
            //return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
