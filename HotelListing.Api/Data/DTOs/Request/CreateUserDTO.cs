using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Data.DTOs.Request
{
    public class CreateUserDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] 
        [StringLength(10, ErrorMessage = "Password cannot be less than two ", MinimumLength = 2)]
        public string Password { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
