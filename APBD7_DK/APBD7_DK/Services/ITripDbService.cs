using APBD7_DK.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBD7_DK.Services
{
    public interface ITripDbService
    {

        Task<IEnumerable<TripDTO>> GetTrips();

        void RegisterClient(ClientPOST client);

        Task<bool> ClientAlreadyRegistered(ClientPOST clientPOST);
        Task<bool> TripExists(ClientPOST clientPOST);

        void CreateClientTrip(ClientPOST clientPOST);
    }
}

