using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CognitiveLocatorDAL.Comunes;

namespace ConsoleDALTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string conn = "Server=qbmjh35pyz.database.windows.net;Database=CognitiveLocator;User Id=rcervantes;Password = Password.1; ";
            Sql aux = new Sql();
            var res = aux.RunStoredProcParams(conn, "SelectAllPerson", null);
        }
    }
}
