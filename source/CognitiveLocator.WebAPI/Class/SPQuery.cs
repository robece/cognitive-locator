using CognitiveLocator.WebAPI.Models;
using CognitiveLocatorDAL.Comunes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;

namespace CognitiveLocator.WebAPI.Class
{
    public class SPQuery
    {
        static string connectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        public async Task<IEnumerable<dynamic>> AddPersonNotFound(Person p)
        {
            return await Sql.RunAsyncStoredProcParams(connectionString, "AddPersonNotFound",
                new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("IdPerson", p.IdPerson),
                    new System.Data.SqlClient.SqlParameter("IsFound", p.IsFound),
                    new System.Data.SqlClient.SqlParameter("Name", p.Name),
                    new System.Data.SqlClient.SqlParameter("LastName", p.LastName),
                    new System.Data.SqlClient.SqlParameter("Alias", p.Alias),
                    new System.Data.SqlClient.SqlParameter("BirthDate", p.BirthDate),
                    new System.Data.SqlClient.SqlParameter("ReportedBy", p.ReportedBy),
                    new System.Data.SqlClient.SqlParameter("Age", p.Age),
                    new System.Data.SqlClient.SqlParameter("Picture", p.Picture),
                    new System.Data.SqlClient.SqlParameter("Location", p.Location),
                    new System.Data.SqlClient.SqlParameter("Notes", p.Notes),
                    new System.Data.SqlClient.SqlParameter("IsActive", p.IsActive),
                    new System.Data.SqlClient.SqlParameter("FaceId", p.FaceId),
                });
        }

        public async Task<IEnumerable<dynamic>> UpdateFoundPerson(string idPerson, int isFound, string location)
        {
            return await Sql.RunAsyncStoredProcParams(connectionString, "UpdateFoundPerson",
                new System.Data.SqlClient.SqlParameter[] 
                {
                    new System.Data.SqlClient.SqlParameter("IdPerson", idPerson),
                    new System.Data.SqlClient.SqlParameter("IsFound", isFound),
                    new System.Data.SqlClient.SqlParameter("Location", location),
                });
        }

        public async Task<IEnumerable<dynamic>> DisablePerson(string idPerson)
        {
            return await Sql.RunAsyncStoredProcParams(connectionString, "DisablePerson",
                new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("IdPerson", idPerson) });
        }

        public async Task<IEnumerable<dynamic>> EnablePerson(string idPerson)
        {
            return await Sql.RunAsyncStoredProcParams(connectionString, "EnablePerson",
                new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("IdPerson", idPerson) });
        }

        public async Task<List<Person>> SelectPersonByName(string name)
        {
            return BuildPersons(await Sql.RunAsyncStoredProcParams(connectionString, "SelectPersonByName",
                new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("Name", name) }));
        }

        public async Task<List<Person>> SelectPersonByLastName(string lastName)
        {
            return BuildPersons(await Sql.RunAsyncStoredProcParams(connectionString, "SelectPersonByLastName",
                new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("LastName", lastName) }));
        }

        public async Task<List<Person>> SelectPersonByNameAndLastName(string name,string lastName)
        {
            return BuildPersons(await Sql.RunAsyncStoredProcParams(connectionString, "SelectPersonByNameAndLastName",
                new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("Name", name), new System.Data.SqlClient.SqlParameter("LastName", lastName) }));
        }

        public async Task<List<Person>> SelectPersonByFaceId(string faceId)
        {
            return BuildPersons(await Sql.RunAsyncStoredProcParams(connectionString, "SelectPersonByFaceId",
                new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("FaceId", faceId) }));
        }

        List<Person> BuildPersons(IEnumerable<dynamic> results)
        {
            List<Person> persons = new List<Person>();

            try
            {
                foreach (dynamic r in results)
                {
                    var person = BuildPersonData(r as IDataRecord);

                    if (person != null)
                    {
                        persons.Add(person);
                    }
                }

                return persons;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Person BuildPersonData(IDataRecord data)
        {
            if (data == null)
                return null;

            return new Person()
            {
                IdPerson = data.GetValueOrDefault<Guid>("IdPerson").ToString(),
                IsFound = data.GetValueOrDefault<int>("IsFound"),
                Name = data.GetValueOrDefault<string>("Name"),
                LastName = data.GetValueOrDefault<string>("LastName"),
                Alias = data.GetValueOrDefault<string>("Alias"),
                Age = data.GetValueOrDefault<int>("Age"),
                BirthDate = data.GetValueOrDefault<DateTime>("BirthDate"),
                ReportedBy = data.GetValueOrDefault<string>("ReportedBy"),
                Picture = data.GetValueOrDefault<string>("Picture"),
                Location = data.GetValueOrDefault<string>("Location"),
                Notes = data.GetValueOrDefault<string>("Notes"),
                IsActive = data.GetValueOrDefault<int>("IsActive"),
                FaceId = data.GetValueOrDefault<Guid>("FaceId").ToString(),
            };
        }
    }

    public static class NullSafeGetter
    {
        public static T GetValueOrDefault<T>(this IDataRecord row, string fieldName)
        {
            int ordinal = row.GetOrdinal(fieldName);
            return row.GetValueOrDefault<T>(ordinal);
        }

        public static T GetValueOrDefault<T>(this IDataRecord row, int ordinal)
        {
            return (T)(row.IsDBNull(ordinal) ? default(T) : row.GetValue(ordinal));
        }
    }
}

