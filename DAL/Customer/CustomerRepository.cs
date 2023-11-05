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
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;

        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            IEnumerable<Customer> customers = Enumerable.Empty<Customer>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    customers = await _context.Customers.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data pelanggan", ex);
                }
            }

            return customers;
        }

        public async Task<Customer?> GetCustomerById(Guid id)
        {
            Customer? customer = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    customer = await _context.Customers.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data pelanggan dengan id: {id}", ex);
                }
            }

            return customer;
        }

        public async Task InsertCustomer(Customer customer)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data pelanggan dengan nama: {customer.CustomerName}.
                        Kode Kustomer '{customer.CustomerCode}' sudah digunakan. Pastikan anda menggunakan kode yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data pelanggan dengan nama: {customer.CustomerName}", ex);
                }
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data pelanggan dengan nama: {customer.CustomerName}.
                        Kode Kustomer '{customer.CustomerCode}' sudah digunakan. Pastikan anda menggunakan kode yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data pelanggan dengan nama: {customer.CustomerName}", ex);
                }
            }
        }

        public async Task DeleteCustomer(Customer customer)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data pelanggan dengan id: {customer.Id}", ex);
                }
            }
        }
    }
}
