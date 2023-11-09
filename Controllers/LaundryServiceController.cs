using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.DTO;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("laundry_service")]
    public class LaundryServiceController : ControllerBase
    {
        private readonly ILogger<LaundryServiceController> _logger;
        private readonly ILaundryServiceRepository _laundryServiceRepository;

        public LaundryServiceController(ILogger<LaundryServiceController> logger, ILaundryServiceRepository laundryServiceRepository)
        {
            _logger = logger;
            _laundryServiceRepository = laundryServiceRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<LaundryService>> GetAllLaundryServices()
        {
            IEnumerable<LaundryService> laundryServices = await _laundryServiceRepository.GetAllLaundryServices();
            return Ok(laundryServices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LaundryService>> GetLaundryServiceById(Guid id)
        {
            LaundryService? laundryService = await _laundryServiceRepository.GetLaundryServiceById(id);
            return Ok(laundryService);
        }

        //[HttpPost("insert")]
        //public async Task<ActionResult<LaundryService>> PostCurrency([FromBody] CurrencyDto currencyDto)
        //{
        //    CurrencyMapper currencyMapper = new();
        //    Currency currency = currencyMapper.CurrencyDtoToCurrency(currencyDto);
        //    await _currencyRepository.InsertCurrency(currency);

        //    return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
        //}

        [HttpPut("update/{id}")]
        public async Task<ActionResult<LaundryService>> UpdateLaundryService(Guid id, [FromBody] LaundryServiceUpdateDto laundryServiceUpdateDto)
        {
            if (id != laundryServiceUpdateDto.Id)
                return BadRequest("ID Tipe Laundry tidak cocok!");

            LaundryService? laundryService = await _laundryServiceRepository.GetLaundryServiceById(id);
            if (laundryService is null)
                return BadRequest($"Tipe Laundry dengan id: {id} tidak ditemukan");

            laundryServiceUpdateDto.PassData(ref laundryService);
            await _laundryServiceRepository.UpdateLaundryService(laundryService);

            return CreatedAtAction(nameof(GetLaundryServiceById), new { id = laundryService.Id }, laundryService);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteLaundryService(Guid id)
        {
            LaundryService? laundryService = await _laundryServiceRepository.GetLaundryServiceById(id);
            if (laundryService is null)
                return BadRequest($"Data Service Laundry dengan id: {id} tidak ditemukan!");

            await _laundryServiceRepository.DeleteLaundryService(laundryService);

            return Ok();
        }
    }
}