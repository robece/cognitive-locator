using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CognitiveLocatorDAL.Comunes;
using CognitiveLocatorDAL.Entidades;
using System.Data.SqlClient;

namespace ConsoleDALTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string conn = "Server=qbmjh35pyz.database.windows.net;Database=CognitiveLocator;User Id=rcervantes;Password = Password.1; ";
            Sql aux = new Sql();
            //var res = aux.RunStoredProcParams(conn, "SelectAllPerson", null);
            PersonDAL personas = new PersonDAL();
            
            personas.Age = 20;
            personas.Alias = "Alias";
            personas.FaceId = 0.0f;
            personas.LastName = "Apellidos";
            personas.Name = "Nombres";
            personas.Notes = "Notas aleatorias";
            personas.Picture = "Ruta del blob";
            personas.RightMargin = 0.0f;
            personas.LeftMargin = 0.0f;
            personas.Height = 0.0f;
            personas.IsActive = 0;
            personas.IsFound = 0;
            personas.Location = "Algun lugar";
            personas.Width = 0.0f;



            var res = aux.RunStoredProcParams(conn, "AddPersonNotFound");
        }
    }
}
