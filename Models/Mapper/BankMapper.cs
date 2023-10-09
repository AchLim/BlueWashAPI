using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class BankMapper
    {
        public partial BankDto BankToBankDto(Bank bank);
        public partial Bank BankDtoToBank(BankDto bankDto);
    }
}
