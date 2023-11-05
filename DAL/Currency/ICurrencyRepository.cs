using System.Collections;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllCurrencies();
        Task<Currency?> GetCurrencyById(Guid id);
        Task InsertCurrency(Currency currency);
        Task UpdateCurrency(Currency currency);
        Task DeleteCurrency(Currency currency);
    }
}