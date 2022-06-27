using System;
using System.Collections.Generic;

namespace APBD7_DK.Models.DTO
{
    public class TripDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public IEnumerable<CountryDTO> Contries { get; set; }
        public IEnumerable<ClientDTO> Clients { get; set; }
    }
}