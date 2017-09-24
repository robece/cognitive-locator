using System;
namespace CognitiveLocator.Models.ApiModels
{
    public class CreateReportModel
    {
        public string Nombre
        {
            get;
            set;
        }

        public string Apellido
        {
            get;
            set;
        }

        public string Alias
        {
            get;
            set;
        }

        public string Edad
        {
            get;
            set;
        }

        public string Ubicacion
        {
            get;
            set;
        }

        public string Notas
        {
            get;
            set;
        }

        /// <summary>
        /// Found = 1
        /// Not Found = 0
        /// </summary>
        /// <value>Encontrado.</value>
        public int Encontrado
        {
            get;
            set;
        }

        public string UrlFormat =>
        String.Format("api/Photo/Post?IsFound={0}&Name={1}&LastName={2}&Alias={3}&Age={4}&Location={5}&Notes={5}",
                      Encontrado, Nombre, Apellido, Alias, Edad, Ubicacion, Notas);
    }
}
