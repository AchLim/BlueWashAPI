using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task<Supplier?> GetSupplierById(Guid id);
        Task InsertSupplier(Supplier supplier);
        Task UpdateSupplier(Supplier supplier);
        Task DeleteSupplier(Supplier supplier);
    }
}