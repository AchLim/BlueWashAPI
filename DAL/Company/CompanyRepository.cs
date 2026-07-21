using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;

namespace WebAPI.DAL
{
    public sealed class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            IEnumerable<Company> companies = Enumerable.Empty<Company>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    companies = await _context.Companies.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data perusahaan", ex);
                }
            }

            return companies;
        }

        public async Task<Company?> GetSingleCompany()
        {
            Company? company = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    company = await _context.Companies.FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data perusahaan", ex);
                }
            }

            return company;
        }

        public async Task<Company?> GetCompanyById(Guid id)
        {
            Company? company = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    company = await _context.Companies.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data perusahaan dengan id: {id}", ex);
                }
            }

            return company;
        }

        public async Task InsertCompany(Company company)
        {
            if (company.Name.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama perusahaan tidak boleh kosong!", null);

            if (company.MobileNumber is not null)
            {
                company.MobileNumber = company.MobileNumber.Trim();
                if (!company.MobileNumber.StartsWith('+'))
                    throw new DatabaseInsertException("Nomor HP harus diawali dengan simbol + (Contoh: +62xxxxxxxxxx)", null);

                if (company.MobileNumber.Length < 7)
                    throw new DatabaseInsertException("Nomor HP tidak valid", null);
            }

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Companies.AddAsync(company);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data perusahaan dengan nama: {company.Name}", ex);
                }
            }
        }

        public async Task UpdateCompany(Company company)
        {
            if (company.Name.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama perusahaan tidak boleh kosong!", null);

            if (company.MobileNumber is not null)
            {
                company.MobileNumber = company.MobileNumber.Trim();
                if (!company.MobileNumber.StartsWith('+'))
                    throw new DatabaseInsertException("Nomor HP harus diawali dengan simbol + (Contoh: +62xxxxxxxxxx)", null);

                if (company.MobileNumber.Length < 7)
                    throw new DatabaseInsertException("Nomor HP tidak valid", null);
            }

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Companies.Update(company);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data perusahaan dengan nama: {company.Name}", ex);
                }
            }
        }

        public async Task DeleteCompany(Company company)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Companies.Remove(company);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data perusahaan dengan id: {company.Id}", ex);
                }
            }
        }
    }
}
