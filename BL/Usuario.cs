using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuarioBusquedaAbierta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuarioBusquedaAbierta.Nombre}','{usuarioBusquedaAbierta.ApellidoPaterno}','{usuarioBusquedaAbierta.ApellidoMaterno}'").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Telefono = obj.Telefono;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;
                            usuario.UserName = obj.UserName;
                            usuario.Password = obj.Password;
                            usuario.Sexo = obj.Sexo;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;
                            usuario.Status =obj.Status.Value;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            result.Objects.Add(usuario);

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
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Email}','{usuario.Telefono}','{usuario.Rol.IdRol}','{usuario.UserName}','{usuario.Password}','{usuario.Sexo}','{usuario.Celular}','{usuario.FechaNacimiento}','{usuario.CURP}','{usuario.Imagen}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");
                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar";
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
        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el registro";
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
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Email}','{usuario.Telefono}',{usuario.Rol.IdRol},'{usuario.UserName}','{usuario.Password}','{usuario.Sexo}','{usuario.Celular}','{usuario.FechaNacimiento}','{usuario.CURP}','{usuario.Imagen}',{usuario.Status},{usuario.Direccion.IdDireccion},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia}");
                                        
                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Actualizar el registro";
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
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Nombre = obj.Nombre;
                        usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        usuario.Email = obj.Email;
                        usuario.Telefono = obj.Telefono;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = obj.IdRol.Value;
                        usuario.Rol.Nombre = obj.NombreRol;
                        usuario.UserName = obj.UserName;
                        usuario.Password = obj.Password;
                        usuario.Sexo = obj.Sexo;
                        usuario.Celular = obj.Celular;
                        usuario.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.CURP = obj.Curp;
                        usuario.Imagen = obj.Imagen;
                        usuario.Status = obj.Status.Value;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = obj.IdDireccion;
                        usuario.Direccion.Calle = obj.Calle;
                        usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                        usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                        usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;


                        result.Object = usuario;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "error al ejecutar GetByIdEF";
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
