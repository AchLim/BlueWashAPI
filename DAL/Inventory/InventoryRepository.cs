using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;

namespace WebAPI.DAL
{
    public sealed class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationContext _context;

        public InventoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            IEnumerable<Inventory> inventories = Enumerable.Empty<Inventory>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    inventories = await _context.Inventories.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data persediaan", ex);
                }
            }

            return inventories;
        }

        public async Task<Inventory?> GetInventoryById(Guid id)
        {
            Inventory? inventory = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    inventory = await _context.Inventories.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data persediaan dengan id: {id}", ex);
                }
            }

            return inventory;
        }

        public async Task InsertInventory(Inventory inventory)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Inventories.AddAsync(inventory);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data persediaan dengan nama: {inventory.ItemName}", ex);
                }
            }
        }

        public async Task UpdateInventory(Inventory inventory)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Inventories.Update(inventory);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data persediaan dengan nama: {inventory.ItemName}", ex);
                }
            }
        }

        public async Task DeleteInventory(Inventory inventory)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Inventories.Remove(inventory);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data persediaan dengan id: {inventory.Id}", ex);
                }
            }
        }
    }
}
