using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public static partial class PurchaseHeaderMapper
    {
        public static partial PurchaseHeaderDto PurchaseHeaderToPurchaseHeaderDto(PurchaseHeader purchaseHeader);
        public static partial PurchaseHeader PurchaseHeaderDtoToPurchaseHeader(PurchaseHeaderDto purchaseHeaderDto);
        public static partial PurchaseHeader PurchaseHeaderUpdateDtoToPurchaseHeader(PurchaseHeaderUpdateDto purchaseHeaderUpdateDto);
    }
}
