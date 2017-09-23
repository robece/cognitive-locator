using CognitiveLocator.WebAPI.Models;
using CognitiveLocatorDAL.Comunes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CognitiveLocator.WebAPI.Class
{
    public class SPQuery
    {
        public async Task<IEnumerable<dynamic>> AddPersonNotFound(Person p)
        {
            string connectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();

            return await Sql.RunAsyncStoredProcParams(connectionString, "AddPersonNotFound",
                new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("IdPerson", p.IdPerson),
                    new System.Data.SqlClient.SqlParameter("IsFound", p.IsFound),
                    new System.Data.SqlClient.SqlParameter("Name", p.Name),
                    new System.Data.SqlClient.SqlParameter("LastName", p.LastName),
                    new System.Data.SqlClient.SqlParameter("Alias", p.Alias),
                    new System.Data.SqlClient.SqlParameter("Age", p.Age),
                    new System.Data.SqlClient.SqlParameter("Picture", p.Picture),
                    new System.Data.SqlClient.SqlParameter("Location", p.Location),
                    new System.Data.SqlClient.SqlParameter("Notes", p.Notes),
                    new System.Data.SqlClient.SqlParameter("IsActive", p.IsActive),
                    new System.Data.SqlClient.SqlParameter("FaceId", p.FaceId),
                    new System.Data.SqlClient.SqlParameter("Height", p.Height),
                    new System.Data.SqlClient.SqlParameter("Width", p.Width),
                    new System.Data.SqlClient.SqlParameter("LeftMargin", p.LeftMargin),
                    new System.Data.SqlClient.SqlParameter("RightMargin", p.RightMargin),
                });
        }
    }
}