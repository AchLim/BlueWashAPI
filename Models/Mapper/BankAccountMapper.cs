using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class BankAccountMapper
    {
        public partial BankAccountDto BankAccountToBankAccountDto(BankAccount bankAccount);
        public partial BankAccount BankAccountDtoToBankAccount(BankAccountDto bankAccountDto);
    }
}
