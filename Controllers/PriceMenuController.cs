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
    public class PriceMenuController : ControllerBase
    {
        private readonly ILogger<PriceMenuController> _logger;
        private readonly IPriceMenuRepository _priceMenuRepository;

        public PriceMenuController(ILogger<PriceMenuController> logger, IPriceMenuRepository priceMenuRepository)
        {
            _logger = logger;
            _priceMenuRepository = priceMenuRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<PriceMenu>> GetAllPriceMenus()
        {
            IEnumerable<PriceMenu> priceMenus = await _priceMenuRepository.GetAllPriceMenus();
            return Ok(priceMenus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PriceMenu>> GetPriceMenuById(Guid id)
        {
            PriceMenu? priceMenu = await _priceMenuRepository.GetPriceMenuById(id);
            return Ok(priceMenu);
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
        public async Task<ActionResult> DeletePriceMenu(Guid id)
        {
            PriceMenu? priceMenu = await _priceMenuRepository.GetPriceMenuById(id);
            if (priceMenu is null)
                return BadRequest($"Data menu harga dengan id: {id} tidak ditemukan!");

            await _priceMenuRepository.DeletePriceMenu(priceMenu);

            return Ok();
        }
    }
}