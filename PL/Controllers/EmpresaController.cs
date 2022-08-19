using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAll();
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.Mensasje = "ocurrio un error " + result.ErrorMessage;
            }
            return View(empresa);
        }
        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            if (IdEmpresa == null)
            {
                IdEmpresa = 0;
            }

            ML.Empresa empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAll();
            if (resultEmpresa.Correct)
            {
                if (IdEmpresa == 0)
                {
                    return View(empresa);
                }
                else
                {
                    ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);
                    if (result.Correct)
                    {
                        empresa = (ML.Empresa)result.Object;
                        return View(empresa);
                    }
                    else
                    {
                        return View("Modal");
                    }
                }
            }
            else
            {
                return View("Modal");
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            if (empresa.IdEmpresa == 0)
            {
                ML.Result result = BL.Empresa.Add(empresa);
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
                ML.Result result = BL.Empresa.Update(empresa);
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

        }
        [HttpGet]
        public ActionResult Delete(ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.Delete(empresa);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado";
            }
            else
            {
                ViewBag.Mensaje = "Error al ejecutar Delete";
            }
            return View("Modal");
        }
    }
}