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
            ML.Result resultEmpresas = BL.Empresa.GetAll();

            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();
            //empleado.Empresa.Empresas = resultEmpresas.Objects;

            ML.Result result = BL.Empleado.GetAll(empleado);
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
                empleado.Empresa.Empresas = resultEmpresas.Objects;
                return View(empleado);
            }
            else
            {
                ViewBag.Mensasje = "ocurrio un error " + result.ErrorMessage;
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            empleado.Nombre = (empleado.Nombre == null) ? "" : empleado.Nombre;
            empleado.ApellidoPaterno = (empleado.ApellidoPaterno == null) ? "" : empleado.ApellidoPaterno;
            empleado.ApellidoMaterno = (empleado.ApellidoMaterno == null) ? "" : empleado.ApellidoMaterno;
            empleado.Empresa.IdEmpresa = (empleado.Empresa.IdEmpresa == null) ? 0 : empleado.Empresa.IdEmpresa;

            ML.Result result = BL.Empleado.GetAll(empleado);
            ML.Result resultEmpresas = BL.Empresa.GetAll();
            empleado.Empresa.Empresas = resultEmpresas.Objects;

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
            if (NumeroEmpleado == null)
            {
                NumeroEmpleado = HttpContext.Session.GetString("NumeroEmpleado");
            }

            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result resultDependientes = BL.Dependientes.GetByIdEmpleado(NumeroEmpleado);
            ML.Result resultEmpleado = BL.Empleado.GetById(NumeroEmpleado);
            ML.Result resultEmpresas = BL.Empresa.GetAll();

            dependiente.Empleado = (ML.Empleado)resultEmpleado.Object;

            if (resultDependientes.Correct)
            {
                HttpContext.Session.SetString("NumeroEmpleado",NumeroEmpleado);
                dependiente.Dependientes = new List<object>();
                dependiente.Dependientes = resultDependientes.Objects;

                return View(dependiente);

            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al cargar los dependientes" + resultDependientes.ErrorMessage;
                return PartialView("Modal");
            }
        }  
        [HttpGet]
        public ActionResult Form(int? IdDependiente)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();
            dependiente.DependienteTipo = new ML.DependienteTipo();
            dependiente.DependienteTipo.DependientesTipo = resultDependienteTipo.Objects;

            if (resultDependienteTipo.Correct)
            {
                if (IdDependiente == null)
                {
                    dependiente.Empleado = new ML.Empleado();
                    dependiente.Empleado.NumeroEmpleado = HttpContext.Session.GetString("NumeroEmpleado").ToString();
                    //dependiente.DependienteTipo = new ML.DependienteTipo();
                    return View(dependiente);
                }
                else
                {
                    ML.Result resultDependiente = BL.Dependientes.GetById(IdDependiente.Value);
                    if (resultDependiente.Correct) 
                    {
                        dependiente = ((ML.Dependiente)resultDependiente.Object);
                        dependiente.DependienteTipo.DependientesTipo = resultDependienteTipo.Objects;
                        return View(dependiente);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al ejecutar GetById" + resultDependiente.ErrorMessage;
                        return View("Modal");
                    }                   
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error en IdDependiente" + resultDependienteTipo.ErrorMessage;
                return View("Modal");
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            if (dependiente.IdDependiente == 0) //add
            {
                result = BL.Dependientes.Add(dependiente);
                if (result.Correct)
                {

                    ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();
                    dependiente.DependienteTipo.DependientesTipo = resultDependienteTipo.Objects;
                    ViewBag.Mensaje = "El Dependiente se agrego Correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El Dependiente no se agrego Correctamente" + result.ErrorMessage;
                }
            }
            else //update
            {
                result = BL.Dependientes.Update(dependiente);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "El Dependiente se actualizo Correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El Dependiente no se actualizo Correctamente" + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int IdDependiente)
        {
            ML.Result result = BL.Dependientes.Delete(IdDependiente);
            if (result.Correct)
            {
                ViewBag.Mensaje = "El dependiente se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }


    }
}
