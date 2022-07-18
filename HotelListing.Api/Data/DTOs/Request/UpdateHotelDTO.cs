using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Data.DTOs.Request
{
    public class UpdateHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 60, MinimumLength = 2, ErrorMessage = "Name is too long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 60, ErrorMessage = "Name is too long")]
        public string Address { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
