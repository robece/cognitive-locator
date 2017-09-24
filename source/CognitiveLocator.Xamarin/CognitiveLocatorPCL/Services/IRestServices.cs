using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CognitiveLocator.Models;
using CognitiveLocator.Models.ApiModels;
using CognitiveLocator.Services;
using Xamarin.Forms;

namespace CognitiveLocator.Services
{
    public interface IRestServices
    {
        Task<bool> CreateReportAsync(CreateReportModel model, byte[] photo);
        Task<List<Person>> SearchPersonByNameAsync(Person person);
        Task<List<Person>> SearchPersonByLastNameAsync(Person person);
        Task<List<Person>> SearchByNameAndLastNameAsync(Person person);
        Task<List<Person>> SearchPersonByPhotoAsync(byte[] photo);
    }
}
