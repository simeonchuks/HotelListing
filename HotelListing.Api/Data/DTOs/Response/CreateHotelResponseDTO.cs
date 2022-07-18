using HotelListing.Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Data.DTOs.Response
{
    public class HotelResponseDTO 
    {
        //public int Id { get; set; }
        public CreateCountryResponseDTO Country { get; set; }
    }

    public class CreateHotelResponseDTO : HotelResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }

    }
}
