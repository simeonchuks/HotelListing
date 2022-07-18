using HotelListing.Api.Data.DTOs.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Data.DTOs.Request
{
    public class UpdateCountryDTO 
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country Name is too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Short Country Name Can not be more than two")]
        public string ShortName { get; set; }

        //public IList<CreateHotelRequestDTO> Hotels { get; set; } YOU CAN ADD THIS IF YOU WANT TO USE THE UPDATE ENDPOINT TO CREATE ALSO
    }
}

