using CognitiveLocator.WebAPI.Models;
using CognitiveLocatorDAL.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CognitiveLocator.WebAPI.Class
{
    public class SPQuery
    {
        public async Task<IEnumerable<dynamic>> AddPersonNotFound(Person p)
        {
            return Sql.RunStoredProcParams("conn", "AddPersonNotFound",
                new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("IsFound", p.IsFound),
                    new System.Data.SqlClient.SqlParameter("Name", p),
                    new System.Data.SqlClient.SqlParameter("LastName", p),
                    new System.Data.SqlClient.SqlParameter("Alias", p),
                    new System.Data.SqlClient.SqlParameter("Age", p),
                    new System.Data.SqlClient.SqlParameter("Picture", p),
                    new System.Data.SqlClient.SqlParameter("Location", p),
                    new System.Data.SqlClient.SqlParameter("Notes", p),
                    new System.Data.SqlClient.SqlParameter("IsActive", p),
                    new System.Data.SqlClient.SqlParameter("FaceId", p),
                    new System.Data.SqlClient.SqlParameter("Height", p),
                    new System.Data.SqlClient.SqlParameter("Width", p),
                    new System.Data.SqlClient.SqlParameter("LeftMargin", p),
                    new System.Data.SqlClient.SqlParameter("RighMargin", p),
                });
        }
    }
}