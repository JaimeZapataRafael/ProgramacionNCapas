using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd'{aseguradora.Nombre}',{aseguradora.Usuario.IdUsuario},'{aseguradora.Imagen}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct =false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraDelete {aseguradora.IdAseguradora}");
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
        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora},'{aseguradora.Nombre}',{aseguradora.Usuario.IdUsuario},'{aseguradora.Imagen}'");
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();

                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Imagen = obj.Imagen;
                            aseguradora.Usuario.UserName = obj.UserName;
                            result.Objects.Add(aseguradora);

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
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var obj = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {IdAseguradora}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        
                        ML.Aseguradora aseguradora = new ML.Aseguradora();

                        aseguradora.IdAseguradora = obj.IdAseguradora;
                        aseguradora.Nombre = obj.Nombre;
                        aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = obj.FechaModificacion.ToString();
                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                        aseguradora.Imagen = obj.Imagen;
                        aseguradora.Usuario.UserName = obj.UserName;

                        result.Object = aseguradora;
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
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}