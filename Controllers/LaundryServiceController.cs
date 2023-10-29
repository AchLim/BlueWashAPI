using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    //[Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("[controller]")]
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

        //[HttpPut("update/{id}")]
        //public async Task<ActionResult<Currency>> UpdateCurrency(Guid id, [FromBody] CurrencyUpdateDto currencyDto)
        //{
        //    if (id != currencyDto.Id)
        //        return BadRequest("ID Mata Uang tidak cocok!");

        //    Currency? currency = await _currencyRepository.GetCurrencyById(id);
        //    if (currency is null)
        //        return BadRequest($"Mata Uang dengan id: {id} tidak ditemukan");

        //    currencyDto.PassData(ref currency);
        //    await _currencyRepository.UpdateCurrency(currency);

        //    return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
        //}

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