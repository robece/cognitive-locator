using System;
namespace CognitiveLocator.Models
{
    public class Person
    {

        public int Id
        {
            get;
            set;
        }

        public string Alias
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }

        public string Apellidos
        {
            get;
            set;
        }

        public int Edad
        {
            get;
            set;
        }

        public string Ubicacion
        {
            get;
            set;
        }

        public string UbicacionURL
        {
            get;
            set;
        }

        public bool Encontrado
        {
            get;
            set;
        }

        // TODO: Blob Storage
        public string PhotoURL
        {
            get;
            set;
        }
	}
}
