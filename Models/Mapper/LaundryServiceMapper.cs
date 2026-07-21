using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public static partial class LaundryServiceMapper
    {
        public static partial LaundryServiceDto LaundryServiceToLaundryServiceDto(LaundryService purchaseHeader);
        public static partial LaundryService LaundryServiceDtoToLaundryService(LaundryServiceDto purchaseHeaderDto);
        public static partial LaundryService LaundryServiceUpdateDtoToLaundryService(LaundryServiceUpdateDto purchaseHeaderUpdateDto);
    }
}
