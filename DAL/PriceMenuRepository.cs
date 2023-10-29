using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;

namespace WebAPI.DAL
{
    public sealed class PriceMenuRepository : IPriceMenuRepository
    {
        private readonly ApplicationContext _context;

        public PriceMenuRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PriceMenu>> GetAllPriceMenus()
        {
            IEnumerable<PriceMenu> priceMenus = Enumerable.Empty<PriceMenu>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    priceMenus = await _context.PriceMenus.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data menu harga", ex);
                }
            }

            return priceMenus;
        }

        public async Task<PriceMenu?> GetPriceMenuById(Guid id)
        {
            PriceMenu? priceMenu = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    priceMenu = await _context.PriceMenus.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data menu harga dengan id: {id}", ex);
                }
            }

            return priceMenu;
        }

        public async Task InsertPriceMenu(PriceMenu priceMenu)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.PriceMenus.AddAsync(priceMenu);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data menu harga dengan nama: {priceMenu.Name}", ex);
                }
            }
        }

        public async Task UpdatePriceMenu(PriceMenu priceMenu)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.PriceMenus.Update(priceMenu);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data menu harga dengan nama: {priceMenu.Name}", ex);
                }
            }
        }

        public async Task DeletePriceMenu(PriceMenu priceMenu)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.PriceMenus.Remove(priceMenu);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data menu harga dengan id: {priceMenu.PriceMenuId}", ex);
                }
            }
        }
    }
}
