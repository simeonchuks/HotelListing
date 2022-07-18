using AutoMapper;
using HotelListing.Api.Configurations.Pagination;
using HotelListing.Api.Data.DTOs.Request;
using HotelListing.Api.Data.DTOs.Response;
using HotelListing.Api.Data.Model;
using HotelListing.Api.Repository;
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
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels([FromQuery] RequestParams requestParams)
        {
            //var hotels = await _unitOfWork.HotelsRepository.GetAll();
            var hotels = await _unitOfWork.HotelsRepository.GetPagedList(requestParams);
            var results = _mapper.Map<IList<CreateHotelResponseDTO>>(hotels);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = await _unitOfWork.HotelsRepository.Get(h => h.Id == id, new List<string> { "Country" });
            var result = _mapper.Map<CreateHotelResponseDTO>(hotel);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        //[Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelRequestDTO hotelRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }

            var hotel = _mapper.Map<Hotel>(hotelRequestDTO);
            await _unitOfWork.HotelsRepository.Insert(hotel);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO updateHotel)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }
           
            var hotel = await _unitOfWork.HotelsRepository.Get(h => h.Id == id);
            if (hotel == null)
            {
                _logger.LogError($"Hotel not found {nameof(UpdateHotel)}");
                return NotFound($"Hotel with {id} not found");
            }

            var hotelToUpdate = _mapper.Map(updateHotel, hotel);
            _unitOfWork.HotelsRepository.Update(hotelToUpdate);
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
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteHotel)}");
                return BadRequest("Submitted data is invalid");
            }
            
            var hotel = await _unitOfWork.HotelsRepository.Get(h => h.Id == id);
            if (hotel == null)
            {
                _logger.LogError($"Hotel not found {nameof(DeleteHotel)}");
                return NotFound($"Hotel with {id} not found");
            }

            await _unitOfWork.HotelsRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
