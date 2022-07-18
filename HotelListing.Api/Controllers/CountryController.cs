using AutoMapper;
using HotelListing.Api.Configurations.Pagination;
using HotelListing.Api.Data.DTOs;
using HotelListing.Api.Data.DTOs.Request;
using HotelListing.Api.Data.DTOs.Response;
using HotelListing.Api.Data.Model;
using HotelListing.Api.Repository;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            //var countries = await _unitOfWork.CountriesRepository.GetAll();
            var countries = await _unitOfWork.CountriesRepository.GetPagedList(requestParams);
            var results = _mapper.Map<IList<CreateCountryResponseDTO>>(countries);
            return Ok(results);
        }

        //[Authorize]
        [HttpGet("{id:int}", Name = "GetCountry")]
        [ResponseCache(CacheProfileName = "120SecondsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _unitOfWork.CountriesRepository.Get(c => c.Id == id, new List<string> { "Hotels" });
            var result = _mapper.Map<CreateCountryResponseDTO>(country);
            return Ok(result); 
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        //[Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryRequestDTO createCountry)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            var country = _mapper.Map<Country>(createCountry);
            await _unitOfWork.CountriesRepository.Insert(country);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO updateCountry)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            var country = await _unitOfWork.CountriesRepository.Get(c => c.Id == id);
            
            if (country == null)
            {
                _logger.LogError($"Country not found {nameof(UpdateCountry)}");
                return NotFound($"Country with {id} not found");
            }

            var countryToUpdate = _mapper.Map(updateCountry, country);
            _unitOfWork.CountriesRepository.Update(countryToUpdate);
            await _unitOfWork.Save();

            //return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
            return NoContent();
        }

        //[Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                return BadRequest("Submitted data is invalid");
            }

            var country = await _unitOfWork.CountriesRepository.Get(c => c.Id == id);
            if (country == null)
            {
                _logger.LogError($"Country not found {nameof(DeleteCountry)}");
                return NotFound($"Country with {id} not found");
            }

            await _unitOfWork.CountriesRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }

        private void Notes()
        {
            /*
             If YOU HAVE A MAPPING THAT INVOLVES CIRCULAR MAPPING, FOR EXAMPLE GET ME ALL THE HOTELS IN THIS COUNTRY, THAT MEANS COUNTRY DTO WILL HAVE A LIST OF HOTELS
             AND YOU WILL ALSO SAY GET ME A PARTICULAR HOTEL IN A SPECIFIC COUNTRY, THAT MEANS THE HOTELRESPONSE DTO WILL HAVE THE OBJECT OF COUNTRY THAT WILL ALLOW IT
             FILTER THE NAME OF THE COUNTRY. THE PROBLEM IS THIS WILL TROW AN ERROR BY DEFAULT. TO FIX THIS WE WILL INSTALL A NUGET PACKAGE:
             MICROSOFT.ASPNETCORE.MVC.NEWTONSOFT.JSON AND THEN CONFIGURE IT IN THE CONTROLLER. IT IS CALLED A REFERENCE LOOP.
              
                
             
            */
        }
    }
}
