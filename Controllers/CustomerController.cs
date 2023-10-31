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
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Customer>> GetAllCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
        {
            Customer? customer = await _customerRepository.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] CustomerDto customerDto)
        {
            CustomerMapper customerMapper = new();
            Customer customer = customerMapper.CustomerDtoToCustomer(customerDto);
            await _customerRepository.InsertCustomer(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, [FromBody] CustomerUpdateDto customerDto)
        {
            if (id != customerDto.Id)
                return BadRequest("ID Pelanggan tidak cocok!");

            Customer? customer = await _customerRepository.GetCustomerById(id);
            if (customer is null)
                return BadRequest($"Pelanggan dengan id: {id} tidak ditemukan");

            customerDto.PassData(ref customer);
            await _customerRepository.UpdateCustomer(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCustomer (Guid id)
        {
            Customer? customer = await _customerRepository.GetCustomerById(id);
            if (customer is null)
                return BadRequest($"Data Pelanggan dengan id: {id} tidak ditemukan!");

            await _customerRepository.DeleteCustomer(customer);

            return Ok();
        }
    }
}