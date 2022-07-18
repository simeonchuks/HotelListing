using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Data.DTOs
{
    public class CreateCountryRequestDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country Name is too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "Short Country Name Can not be more than two")]
        public string ShortName { get; set; }
    }
}
