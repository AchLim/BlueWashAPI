using EntityFramework.Exceptions.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;

namespace WebAPI.DAL
{
    public sealed class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationContext _context;

        public SupplierRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            IEnumerable<Supplier> suppliers = Enumerable.Empty<Supplier>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    suppliers = await _context.Suppliers.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data pemasok", ex);
                }
            }

            return suppliers;
        }

        public async Task<Supplier?> GetSupplierById(Guid id)
        {
            Supplier? supplier = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    supplier = await _context.Suppliers.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data pemasok dengan id: {id}", ex);
                }
            }

            return supplier;
        }

        public async Task InsertSupplier(Supplier supplier)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Suppliers.AddAsync(supplier);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data pemasok dengan nama: {supplier.SupplierName}.
                        Kode Kustomer '{supplier.SupplierCode}' sudah digunakan. Pastikan anda menggunakan kode yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data pemasok dengan nama: {supplier.SupplierName}", ex);
                }
            }
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Suppliers.Update(supplier);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data pemasok dengan nama: {supplier.SupplierName}.
                        Kode Kustomer '{supplier.SupplierCode}' sudah digunakan. Pastikan anda menggunakan kode yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data pemasok dengan nama: {supplier.SupplierName}", ex);
                }
            }
        }

        public async Task DeleteSupplier(Supplier supplier)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Suppliers.Remove(supplier);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data pemasok dengan id: {supplier.Id}", ex);
                }
            }
        }
    }
}
