using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]// GET: Usuario / action verb
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
           
            ML.Result result = BL.Usuario.GetAll(usuario);


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
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
            ML.Result result = BL.Usuario.GetAll(usuario);
            //ML.Result resultApi = new ML.Result();

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(_configuration["WebApi"]);
            //    var responseTask = client.GetAsync("api/Usuario/GetAll");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                    //var readTask = result.Content.ReadAsAsync<ML.Result>();
                    //readTask.Wait();

                    //resultApi.Objects = new List<object>();
                    
                    //foreach (var resultItem in readTask.Result.Objects)
                    //{
                    //    ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                    //    resultApi.Objects.Add(resultUsuario);
                    //}
                }
            //}
            else
            {
                result.Correct = false;
            }

            //usuario.Usuarios = resultApi.Objects;
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
                    //ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                    ML.Result resultApi = new ML.Result();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);
                        var responseTask = client.GetAsync("api/Usuario/GetById?IdUsuario="+IdUsuario);
                        responseTask.Wait();

                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            var resultItem = readTask.Result.Object;
                            
                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            resultApi.Object = resultUsuario;
                        }
                    }

                    usuario = (ML.Usuario)resultApi.Object;

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
            if (ModelState.IsValid)            
            {
                IFormFile imagen = Request.Form.Files["fuImage"];
                if (imagen != null)
                {
                    byte[] ImagenByte = ConvertToBytes(imagen);
                    usuario.Imagen = Convert.ToBase64String(ImagenByte);

                }

                if (usuario.IdUsuario == 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);
                        var postTask = client.PostAsJsonAsync<ML.Usuario>("api/Usuario/Add",usuario);
                        postTask.Wait();

                        var resultService = postTask.Result;

                        //ML.Result result = BL.Usuario.Add(usuario);

                        if (resultService.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Registro Exitoso";
                        }
                        else
                        {
                            ViewBag.Mensaje = "Ocurrio un Error";
                        }
                        return View("Modal");
                    }
                }

                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);
                        var postTask = client.PostAsJsonAsync<ML.Usuario>("api/Usuario/Update", usuario);
                        postTask.Wait();

                        var resultService = postTask.Result;

                        //ML.Result result = BL.Usuario.Update(usuario);
                        if (resultService.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Registro Actualizado";
                        }
                        else
                        {
                            ViewBag.Mensaje = "Ocurrio un Error";
                        }
                    }
                    return View("Modal");
                }
            }
            else
            {
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();
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

        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            //ML.Result result = BL.Usuario.Delete(usuario);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebApi"]);
                var responseTask = client.DeleteAsync("api/Usuario/Delete/?IdUsuario=" + IdUsuario);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Registro Eliminado";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un Error";
                }
            }
            return View("Modal");
        }
        [HttpGet]
        public ActionResult UpdateStatus(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetById(IdUsuario);
            if (result.Correct)
            {
                usuario = (ML.Usuario)result.Object;
                usuario.Status = (usuario.Status) ? false : true;
                ML.Result resultUpdate = BL.Usuario.Update(usuario);
                if (resultUpdate.Correct)
                {
                    ViewBag.Mensaje = "Estatus Actualizado ";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un Error al actualizar el status" + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un Error al actualizar el status" + result.ErrorMessage;
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
