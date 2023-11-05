using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;

namespace WebAPI.DAL
{
    public sealed class LaundryServiceRepository : ILaundryServiceRepository
    {
        private readonly ApplicationContext _context;

        public LaundryServiceRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LaundryService>> GetAllLaundryServices()
        {
            IEnumerable<LaundryService> laundryServices = Enumerable.Empty<LaundryService>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    laundryServices = await _context.LaundryServices.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data Laundry Service", ex);
                }
            }

            return laundryServices;
        }

        public async Task<LaundryService?> GetLaundryServiceById(Guid id)
        {
            LaundryService? laundryService = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    laundryService = await _context.LaundryServices.Include(ls => ls.PriceMenus).FirstOrDefaultAsync(ls => ls.Id == id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data Laundry Service dengan id: {id}", ex);
                }
            }

            return laundryService;
        }

        public async Task InsertLaundryService(LaundryService laundryService)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.LaundryServices.AddAsync(laundryService);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data Laundry Service dengan nama: {laundryService.Name}", ex);
                }
            }
        }

        public async Task UpdateLaundryService(LaundryService laundryService)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.LaundryServices.Update(laundryService);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data Service Laundry dengan nama: {laundryService.Name}", ex);
                }
            }
        }

        public async Task DeleteLaundryService(LaundryService laundryService)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.LaundryServices.Remove(laundryService);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data Service Laundry dengan id: {laundryService.Id}", ex);
                }
            }
        }
    }
}
