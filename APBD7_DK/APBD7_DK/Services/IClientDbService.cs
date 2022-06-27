using APBD7_DK.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBD7_DK.Services
{
    public interface IClientDbService
    {
        void DeleteClient(int idClient);
        Task<bool> ClientExists(int idClient);
    }
}
