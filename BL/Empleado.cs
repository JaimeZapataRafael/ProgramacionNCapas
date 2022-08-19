using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {empleado.NumeroEmpleado}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.NSS}','{empleado.FechaIngreso}','{empleado.Foto}',{empleado.Empresa.IdEmpresa}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll(ML.Empleado empleadoBusquedaAbierta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleadoBusquedaAbierta.Nombre}','{empleadoBusquedaAbierta.ApellidoPaterno}','{empleadoBusquedaAbierta.ApellidoMaterno}','{empleadoBusquedaAbierta.Empresa.IdEmpresa}'").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.Nombre = obj.Nombre;
                            empleado.RFC = obj.Rfc;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                            empleado.Foto = obj.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.NombreEmpresa;

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var obj = context.Empleados.FromSqlRaw($"EmpleadoGetById {NumeroEmpleado}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = obj.NumeroEmpleado;
                        empleado.Nombre = obj.Nombre;
                        empleado.RFC = obj.Rfc;
                        empleado.ApellidoPaterno = obj.ApellidoPaterno;
                        empleado.ApellidoMaterno = obj.ApellidoMaterno;
                        empleado.Email = obj.Email;
                        empleado.Telefono = obj.Telefono;
                        empleado.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        empleado.NSS = obj.Nss;
                        empleado.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                        empleado.Foto = obj.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                        empleado.Empresa.Nombre = obj.NombreEmpresa;

                        result.Object = empleado;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
