using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;

namespace WebAPI.DAL
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationContext _context;

        public CurrencyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            IEnumerable<Currency> currencies = Enumerable.Empty<Currency>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    currencies = await _context.Currencies.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data Mata Uang", ex);
                }
            }

            return currencies;
        }

        public async Task<Currency?> GetCurrencyById(Guid id)
        {
            Currency? currency = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    currency = await _context.Currencies.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data Mata Uang dengan id: {id}", ex);
                }
            }

            return currency;
        }

        public async Task InsertCurrency(Currency currency)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Currencies.AddAsync(currency);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data Mata Uang dengan nama: {currency.Name}", ex);
                }
            }
        }

        public async Task UpdateCurrency(Currency currency)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Currencies.Update(currency);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data Mata Uang dengan nama: {currency.Name}", ex);
                }
            }
        }

        public async Task DeleteCurrency(Currency currency)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Currencies.Remove(currency);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data Mata Uang dengan id: {currency.Id}", ex);
                }
            }
        }
    }
}
