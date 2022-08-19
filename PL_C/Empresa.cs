using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Empresa
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            StreamReader archivo = new StreamReader(@"C:\Users\digis\Documents\LayoutEmpresa.txt");
            string line;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<Object>();
            line = archivo.ReadLine();

            while ((line = archivo.ReadLine()) != null)
            {
                string[] datos = line.Split('|');
                ML.Empresa empresa = new ML.Empresa();
                empresa.Nombre = datos[0];
                empresa.Telefono = datos[1];
                empresa.Email = datos[2];
                empresa.DireccionWeb = datos[3];
                //empresa.Logo = datos[4];

                result = BL.Empresa.Add(empresa);
                if (result.Correct == false)
                {
                    resultErrores.Objects.Add
                        (
                        "No se inserto el Nombre: " + empresa.Nombre + " " +
                        "No se inserto el Telefono: " + empresa.Telefono + " " +
                        "No se inserto el Email: " + empresa.Email + " " +
                        "No se inserto la Direccion Web: " + empresa.DireccionWeb + " " +
                        "No se inserto el Logo: " + empresa.Logo + " "
                        );
                }
            }

            if (resultErrores.Objects != null)
            {
                TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\ErroresCargaMasiva.txt");

                foreach (string error in resultErrores.Objects)
                {
                    tw.WriteLine(error);
                    Console.WriteLine(error);
                }
                tw.Close();
            }


            return result;
        }

    }
}
