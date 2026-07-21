using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public static partial class SalesHeaderMapper
    {
        public static partial SalesHeaderDto SalesHeaderToSalesHeaderDto(SalesHeader purchaseHeader);
        public static partial SalesHeader SalesHeaderDtoToSalesHeader(SalesHeaderDto purchaseHeaderDto);
        public static partial SalesHeader SalesHeaderUpdateDtoToSalesHeader(SalesHeaderUpdateDto purchaseHeaderUpdateDto);
    }
}
