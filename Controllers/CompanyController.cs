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
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ILogger<CompanyController> logger, ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Company>> GetAllCompanies()
        {
            IEnumerable<Company> companies = await _companyRepository.GetAllCompanies();
            return Ok(companies);
        }

        [HttpGet("single")]
        public async Task<ActionResult<Company>> GetSingleCompany()
        {
            Company? company = await _companyRepository.GetSingleCompany();
            if (company is null)
            {
                return BadRequest("[COM-ERR-001] Terjadi kesalahan dalam pengambilan data perusahaan. Silahkan menghubungi pihak developer terkait masalah ini.");
            }
            return Ok(company);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(Guid id)
        {
            Company? company = await _companyRepository.GetCompanyById(id);
            return Ok(company);
        }

        [HttpPost("insert")]
        public ActionResult<Company> PostCompany([FromBody] CompanyDto companyDto)
        {
            return BadRequest("Action prohibited!");
            //CompanyMapper companyMapper = new();
            //Company company = companyMapper.CompanyDtoToCompany(companyDto);
            //await _companyRepository.InsertCompany(company);

            //return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Company>> UpdateCompany(Guid id, [FromBody] CompanyUpdateDto companyDto)
        {
            if (id != companyDto.Id)
                return BadRequest("ID Perusahaan tidak cocok!");

            Company? company = await _companyRepository.GetCompanyById(id);
            if (company is null)
                return BadRequest($"Perusahaan dengan id: {id} tidak ditemukan");

            companyDto.PassData(ref company);
            await _companyRepository.UpdateCompany(company);

            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteCompany (Guid id)
        {
            return BadRequest("Action prohibited!");
            //Company? company = await _companyRepository.GetCompanyById(id);
            //if (company is null)
            //    return BadRequest($"Data perusahaan dengan id: {id} tidak ditemukan!");

            //await _companyRepository.DeleteCompany(company);

            //return Ok();
        }
    }
}