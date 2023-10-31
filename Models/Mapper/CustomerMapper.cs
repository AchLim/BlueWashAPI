using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class CustomerMapper
    {
        public partial CustomerDto CustomerToCustomerDto(Customer customer);
        public partial Customer CustomerDtoToCustomer(CustomerDto customerDto);
        public partial Customer CustomerUpdateDtoToCustomer(CustomerUpdateDto customerDto);
    }
}
