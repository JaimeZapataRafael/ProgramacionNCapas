using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Controllers

{
    public class AseguradoraController : Controller
    {
        [HttpGet]// GET: Aseguradora / action verb
        public ActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                ViewBag.Mensasje = "ocurrio un error " + result.ErrorMessage;
            }

            return View(aseguradora);
        }
        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            if (IdAseguradora == null)
            {
                IdAseguradora = 0;
            }
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuario = BL.Usuario.GetAll(usuario);

            if (resultUsuario.Correct)
            {
                if (IdAseguradora == 0)
                {
                    aseguradora.Usuario = new ML.Usuario();
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                    return View(aseguradora);
                }
                else
                {
                    ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);
                    if (result.Correct)
                    {
                        aseguradora = (ML.Aseguradora)result.Object;


                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                        return View(aseguradora);
                    }
                    else
                    {
                        return View("Modal");
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un problema: " + resultUsuario.ErrorMessage;
                return View("Modal");
            }


        }
        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            if (aseguradora.IdAseguradora == 0)
            {
                ML.Result result = BL.Aseguradora.Add(aseguradora);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro Exitoso";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un Error " + result.ErrorMessage;
                }
                return View("Modal");
            }
            else
            {
                ML.Result result = BL.Aseguradora.Update(aseguradora);
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
        public ActionResult Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Delete(aseguradora);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado";
            }
            else
            {
                ViewBag.Mensaje = "Error al ejecutar DeleteEF";
            }
            return View("Modal");
        }

        //JSONS
        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}