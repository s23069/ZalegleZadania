using APBD7_DK.Models;
using APBD7_DK.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7_DK.Services
{
    public class ClientDbService : IClientDbService
    {
        private readonly masterContext _dbContext;
        public ClientDbService(masterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ClientExists(int idClient) 
        {
            var clientHasTrip = await _dbContext.ClientTrips.AnyAsync(t => t.IdClient == idClient);
            var clientExists = await _dbContext.Clients.AnyAsync(t => t.IdClient == idClient);
            if (clientHasTrip == true || clientExists == false)
                return false;

            return true;


        }

        public async void DeleteClient(int idClient)
        {

            var client = await _dbContext.Clients.SingleOrDefaultAsync(dbClient => dbClient.IdClient == idClient);
            _dbContext.Clients.Remove(client);

            _dbContext.SaveChanges();
            
        }




    }
}
