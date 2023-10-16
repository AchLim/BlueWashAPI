using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class CurrencyMapper
    {
        public partial CurrencyDto CurrencyToCurrencyDto(Currency currency);
        public partial Currency CurrencyDtoToCurrency(CurrencyDto currencyDto);
        public partial Currency CurrencyUpdateDtoToCurrency(CurrencyUpdateDto currencyDto);
    }
}
