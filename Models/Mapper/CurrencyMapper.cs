using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class CurrencyMapper
    {
        public partial CurrencyDto CurrencyToCurrencyDto(Currency currency);
        public partial Currency CurrencyDtoToCurrency(CurrencyDto currencyDto);
    }
}
