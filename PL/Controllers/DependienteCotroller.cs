using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DependienteController : Controller
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
        public ActionResult GetByIdEmpleado(string NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Dependiente dependiente = new ML.Dependiente();
            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.GetAll();
            if (resultEmpresa.Correct)
            {              

                    ML.Result result = BL.Dependientes.GetByIdEmpleado(NumeroEmpleado);
                //ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
                if (result.Correct)
                {
                    dependiente.Dependientes = result.Objects;
                }
                else
                {
                    ViewBag.Mensasje = "ocurrio un error " + result.ErrorMessage;
                }
                return View(dependiente);

            }
            else
            {
                return View("Modal");
            }

        }

    }
}
