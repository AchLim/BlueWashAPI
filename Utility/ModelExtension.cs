using WebAPI.DTO;
using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Utility
{
    public static class ModelExtension
    {
        public static void PassData(this CurrencyUpdateDto dto, ref Currency currency)
        {
            currency.Name = dto.Name;
            currency.Code = dto.Code;
            currency.CultureName = dto.CultureName;
        }

        public static void PassData(this LaundryServiceUpdateDto dto, ref LaundryService laundryService)
        {
            laundryService.Name = dto.Name;

            var result = 0;

            if (dto.LaundryProcessWash)
                result |= (ushort)LaundryProcess.Wash;

            if (dto.LaundryProcessDry)
                result |= (ushort)LaundryProcess.Dry;

            if (dto.LaundryProcessIron)
                result |= (ushort)LaundryProcess.Iron;

            laundryService.LaundryProcess = (LaundryProcess)result;
        }
        public static void PassData(this CustomerUpdateDto dto, ref Customer customer)
        {
            customer.CustomerName = dto.CustomerName;
            customer.CustomerCode = dto.CustomerCode;
            customer.CustomerAddress = dto.CustomerAddress;
            customer.CurrencyId = dto.CurrencyId;
        }


        public static bool IsNotEmpty<T>(this ICollection<T>? collections)
        {
            return collections != null && collections.Count > 0;
        }
    }
}
