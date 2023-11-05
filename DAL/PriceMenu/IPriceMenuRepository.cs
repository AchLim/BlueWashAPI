using System.Collections;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IPriceMenuRepository
    {
        Task<IEnumerable<PriceMenu>> GetAllPriceMenus();
        Task<PriceMenu?> GetPriceMenuById(Guid id);
        Task InsertPriceMenu(PriceMenu priceMenu);
        Task UpdatePriceMenu(PriceMenu priceMenu);
        Task DeletePriceMenu(PriceMenu priceMenu);
    }
}