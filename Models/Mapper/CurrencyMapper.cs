using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class CurrencyMapper
    {
        [MapperIgnoreSource("Id")]
        public partial CurrencyDto CurrencyToCurrencyDto(Currency currency);

        [MapperIgnoreTarget("Id")]
        public partial Currency CurrencyDtoToCurrency(CurrencyDto currencyDto);
    }
}
