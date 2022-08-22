using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependientes
    {
        public static ML.Result GetByIdEmpleado(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Dependientes.FromSqlRaw($"DependienteGetByEmpleado {NumeroEmpleado}").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;
                            result.Objects.Add(dependiente);
                            result.Correct = true;
                        }
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
        public static ML.Result GetById(int IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var obj = context.Dependientes.FromSqlRaw($"DependienteGetById {IdDependiente}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {

                        ML.Dependiente dependiente = new ML.Dependiente();

                        dependiente.IdDependiente = obj.IdDependiente;
                        dependiente.Nombre = obj.Nombre;
                        dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                        dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                        dependiente.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        dependiente.EstadoCivil = obj.EstadoCivil;
                        dependiente.Genero = obj.Genero;
                        dependiente.Telefono = obj.Telefono;
                        dependiente.RFC = obj.Rfc;
                        dependiente.Empleado = new ML.Empleado();
                        dependiente.Empleado.NumeroEmpleado = obj.NumeroEmpleado;
                        dependiente.DependienteTipo = new ML.DependienteTipo();
                        dependiente.DependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;


                        result.Object = dependiente;
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
        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DependienteAdd'{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}','{dependiente.FechaNacimiento}','{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.RFC}','{dependiente.DependienteTipo.IdDependienteTipo}','{dependiente.Empleado.NumeroEmpleado}'");
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
        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DependienteUpdate '{dependiente.IdDependiente}','{dependiente.Nombre}','{dependiente.ApellidoPaterno}','{dependiente.ApellidoMaterno}','{dependiente.FechaNacimiento}','{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.RFC}','{dependiente.DependienteTipo.IdDependienteTipo}','{dependiente.Empleado.NumeroEmpleado}'");
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
        public static ML.Result Delete(int? IdDependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DependienteDelete {IdDependiente}");
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
    }
}
