using AutoMapper;
using HotelListing.Api.Data.DTOs;
using HotelListing.Api.Data.DTOs.Request;
using HotelListing.Api.Data.DTOs.Response;
using HotelListing.Api.Data.Model;
using HotelListing.Api.Data.Model.Authenticate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Configurations.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCountryRequestDTO, Country>();
            CreateMap<Country, CreateCountryResponseDTO>();

            CreateMap<CreateHotelRequestDTO, Hotel>();
            CreateMap<Hotel, CreateHotelResponseDTO>();

            CreateMap<UpdateHotelDTO, Hotel>();

            //USER
            CreateMap<CreateUserDTO, User>();

        }
    }
}
