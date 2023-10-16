//using Riok.Mapperly.Abstractions;
//using WebAPI.Models.DTO;

//namespace WebAPI.Models.Mapper
//{
//    [Mapper]
//    public partial class BankAccountMapper
//    {
//        [MapperIgnoreSource("Id")]
//        [MapperIgnoreSource("Bank")]
//        public partial BankAccountDto BankAccountToBankAccountDto(BankAccount bankAccount);


//        [MapperIgnoreTarget("Id")]
//        [MapperIgnoreTarget("Bank")]
//        public partial BankAccount BankAccountDtoToBankAccount(BankAccountDto bankAccountDto);

//        [MapperIgnoreTarget("Bank")]
//        public partial BankAccount BankAccountUpdateDtoToBankAccount(BankAccountUpdateDto bankAccountDto);
//    }
//}
