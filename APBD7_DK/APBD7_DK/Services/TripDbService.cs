using APBD7_DK.Models;
using APBD7_DK.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD7_DK.Services
{
    public class TripDbService : ITripDbService
    {
        private readonly masterContext _dbContext;


        public TripDbService(masterContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<TripDTO>> GetTrips()
        {
            var trips = await _dbContext.Trips
                .Include(e => e.ClientTrips)
                .Include(e => e.CountryTrips)
                .Select(e => new TripDTO
                {
                    Name = e.Name,
                    Description = e.Description,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Contries = e.CountryTrips.Select(e => new CountryDTO { Name = e.IdCountryNavigation.Name }).ToList(),
                    Clients = e.ClientTrips.Select(e => new ClientDTO { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName })
                }

                ).OrderByDescending(e => e.DateFrom).ToListAsync();

            return trips;


        }
        public async void RegisterClient(ClientPOST clientP)
        {
            if (!_dbContext.Clients.Any(c => c.Pesel.Equals(clientP.Pesel)))
                AddNewClient(clientP);
        }



        public async void AddNewClient(ClientPOST clientP)
        {
            await _dbContext.Clients.AddAsync(new Client
            {
                // IdClient = _dbContext.Clients.Max(c => c.IdClient) + 1,
                FirstName = clientP.FirstName,
                LastName = clientP.LastName,
                Email = clientP.Email,
                Telephone = clientP.Telephone,
                Pesel = clientP.Pesel,

            });

            _dbContext.SaveChanges();
        }


        public async Task<bool> ClientAlreadyRegistered(ClientPOST clientP)
        {
            var client = await _dbContext.Clients.SingleOrDefaultAsync(c => c.Pesel == clientP.Pesel);

            return await _dbContext.ClientTrips.AnyAsync(trip =>
                trip.IdClient == client.IdClient && trip.IdTrip == clientP.IdTrip);
        }

        public async Task<bool> TripExists(ClientPOST clientPOST)
        {
            return await _dbContext.Trips.AnyAsync(t => t.IdTrip == clientPOST.IdTrip && t.Name.Equals(clientPOST.TripName));
        }


        public void CreateClientTrip(ClientPOST clientP)
        {
            var client = _dbContext.Clients.SingleOrDefault(c => c.Pesel == clientP.Pesel);
            Console.Write(client.FirstName + client.LastName);

            _dbContext.ClientTrips.Add(new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = clientP.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientP.PaymentDate,
            });

            _dbContext.SaveChanges();
        }
    }
}