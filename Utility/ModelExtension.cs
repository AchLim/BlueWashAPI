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

        public static bool IsNotEmpty<T>(this ICollection<T>? collections)
        {
            return collections != null && collections.Count > 0;
        }
    }
}
