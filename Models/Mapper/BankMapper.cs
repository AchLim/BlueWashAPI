using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class BankMapper
    {
        [MapperIgnoreSource("Id")]
        public partial BankDto BankToBankDto(Bank bank);

        [MapperIgnoreTarget("Id")]
        public partial Bank BankDtoToBank(BankDto bankDto);
    }
}
