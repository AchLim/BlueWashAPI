using System.Collections;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory?> GetInventoryById(Guid id);
        Task InsertInventory(Inventory inventory);
        Task UpdateInventory(Inventory inventory);
        Task DeleteInventory(Inventory inventory);
    }
}