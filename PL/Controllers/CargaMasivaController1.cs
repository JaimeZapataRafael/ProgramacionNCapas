using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult CargaMasivaEmpresa()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public IActionResult CargaMasivaEmpresa(ML.Empresa empresa)
        {
            IFormFile archivo = Request.Form.Files["FileExcel"]; 

            if (HttpContext.Session.GetString("PathArchivo") == null) // valida si existe el archivo
            { 
                if (archivo != null)
                
                {
                    if (archivo.Length > 0) // valida si trae informacion
                    {
                        string FileName = Path.GetFileName(archivo.FileName); //nombre del archivo
                        string folderPath = _configuration["PathFolder:value"]; // donde esta guardado
                        string extensioArchivo = Path.GetExtension(archivo.FileName).ToLower(); //extension y cambia aminusculas el mombre
                        string extensionModulo = _configuration["TipoExcel"];  //Varible Globla

                        if (extensioArchivo == extensionModulo) //compara el nombre y verifica que el archivo sea valido
                        {
                            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(FileName)) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"; // edita el nombre con fecha y hora para que no existan duplicados
                            if (!System.IO.File.Exists(filePath)) // valida si existe el archivo en la ruta
                            {
                                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                                {
                                    archivo.CopyTo(stream);  //Guardar una copia de mi archivo
                                }

                                string connectionString = _configuration["ConnectionStringExcel:value"] + filePath; //validas la conexion + ruta

                                ML.Result resultEmpresa = BL.Empresa.ConvertirExcelDataTable(connectionString); // se llama al metodo de bl

                                if (resultEmpresa.Correct)
                                {
                                    ML.Result resultValidacion = BL.Empresa.ValidarExcel(resultEmpresa.Objects);
                                    if (resultValidacion.Objects.Count == 0)  //No Errores
                                    {
                                        resultValidacion.Correct = true;
                                        HttpContext.Session.SetString("PathArchivo", filePath);
                                    }


                                    return View(resultValidacion);

                                }
                                else
                                {
                                    ViewBag.Message = "No se encontraron registros / Tenia Errores";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Seleccione un archivo valido (.xlsx)";
                        }


                    }
                    else
                    {

                        ViewBag.Mesaage = "No tiene datos el archivo";
                    }         
                }
                else
                {
                    ViewBag.Message = "No selecciono un archivo";
                }
            }

           else
           {                
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Empresa.ConvertirExcelDataTable(connectionString);
                if (resultData.Correct)
                {
                     ML.Result resultErrores = new ML.Result();
                     resultErrores.Objects = new List<Object>();

                     foreach (ML.Empresa EmpresaItem in resultData.Objects)                        
                     {                        
                         ML.Result resultAdd = BL.Empresa.Add(EmpresaItem);
                         if (!resultAdd.Correct)
                         {                           
                           resultErrores.Objects.Add("No se inserto la empresa con el nombre:  " + EmpresaItem.Nombre + " Nose se inserto la empresa con el telefono: " + EmpresaItem.Telefono + " Nose se inserto la empresa con el email: " + EmpresaItem.Email + " Nose se inserto la empresa con la direccion web: " + EmpresaItem.DireccionWeb + " Nose se inserto la empresa con el logo: " + EmpresaItem.Logo);
                         }
                     }

                     if (resultErrores.Objects.Count > 0)
                     {                        
                        string folderError = _configuration["PathFolderError:value"];
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, folderError + @"\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {

                            foreach (string ln in resultErrores.Objects)
                            {                                
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Mensaje = "Algunas empresas no han sido registrados correctamente";
                     }
                        else
                        {
                            ViewBag.Mensaje = "Las Empresas han sido registrados correctamente";
                        }

                    }


           }
            return View("Modal");
        }
    }
}
