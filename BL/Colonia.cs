﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Colonia
    {
        static public ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RJaimeProgramacionNCapasContext context = new DL.RJaimeProgramacionNCapasContext())
                {
                    var query = context.Colonia.FromSqlRaw($"ColoniaByIdMunicipio {IdMunicipio}").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {

                        foreach (var obj in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre;
                            colonia.CodigoPostal = obj.CodigoPostal;
                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;

                            result.Objects.Add(colonia);
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
    }
}
