using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result result = BL.Empleado.GetAll(empleado);           
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Mensasje = "ocurrio un error " + result.ErrorMessage;
            }
            return View(empleado);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.ApellidoMaterno = (empleado.ApellidoMaterno == null) ? "" : empleado.ApellidoMaterno;
            empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == null) ? 0 : empleado.Empresa.IdEmpresa;
            ML.Result result = BL.Empleado.GetAll(empleado);
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al llamar getall con datos" + result.ErrorMessage;
            }
            return View(empleado);
        }
        [HttpGet]
        public ActionResult Form(string NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAll();
            if (resultEmpresa.Correct)
            {
                if (NumeroEmpleado == null)
                {
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    empleado.Action = "Add";
                    return View(empleado);
                }
                else
                {
                    ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
                    if (result.Correct)
                    {
                        empleado = (ML.Empleado)result.Object;
                        empleado.Action = "Update";
                        empleado.Empresa.Empresas=resultEmpresa.Objects;
                        return View(empleado);
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
        public ActionResult Form(ML.Empleado empleado)
        {
            if (empleado.Action == "Add")
            {
                ML.Result result = BL.Empleado.Add(empleado);
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
                if (empleado.Action == "Update")
                {
                    ML.Result result = BL.Empleado.Update(empleado);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Registro Exitoso";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un Error " + result.ErrorMessage;
                    }
                }
                
                return View("Modal");
            }

        }
        [HttpGet]
        public ActionResult Delete(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Delete(empleado);
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
