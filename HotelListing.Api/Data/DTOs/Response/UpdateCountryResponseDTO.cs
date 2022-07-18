using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Data.DTOs.Response
{
    public class UpdateCountryResponseDTO
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public IList<CreateHotelResponseDTO> Hotels { get; set; }
    }
}
