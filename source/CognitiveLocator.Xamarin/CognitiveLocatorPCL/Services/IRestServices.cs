using System;
using System.Threading.Tasks;
using CognitiveLocator.Models.ApiModels;
using CognitiveLocator.Services;
using Xamarin.Forms;

namespace CognitiveLocator.Services
{
    public interface IRestServices
    {
        Task<bool> CreateReportAsync(CreateReportModel model, byte[] photo);
    }
}
