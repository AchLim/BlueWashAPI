using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class CompanyMapper
    {
        public partial CompanyDto CompanyToCompanyDto(Company customer);
        public partial Company CompanyDtoToCompany(CompanyDto customerDto);
        public partial Company CompanyUpdateDtoToCompany(CompanyUpdateDto customerDto);
    }
}
