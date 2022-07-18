using HotelListing.Api.Data.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Services.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(UserLoginDTO loginDTO);
        Task<string> CreateToken();

    }
}
