using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public sealed class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private readonly ApplicationContext _context;

        public ChartOfAccountRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChartOfAccount>> GetAllChartOfAccounts()
        {
            IEnumerable<ChartOfAccount> ChartOfAccounts = Enumerable.Empty<ChartOfAccount>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ChartOfAccounts = await _context.ChartOfAccounts
                                                    .OrderBy(coa => coa.AccountHeaderNo)
                                                    .ThenBy(coa => coa.AccountNo)
                                                    .ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data daftar kode akun", ex);
                }
            }

            return ChartOfAccounts;
        }

        public async Task<ChartOfAccount?> GetChartOfAccountById(Guid id)
        {
            ChartOfAccount? ChartOfAccount = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ChartOfAccount = await _context.ChartOfAccounts.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data pelanggan dengan id: {id}", ex);
                }
            }

            return ChartOfAccount;
        }

        public async Task InsertChartOfAccount(ChartOfAccount ChartOfAccount)
        {
            if (ChartOfAccount.AccountHeaderName.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama Header Akun tidak boleh kosong!", null);

            if (ChartOfAccount.AccountName.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama Detail Akun tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.ChartOfAccounts.AddAsync(ChartOfAccount);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data kode akun dengan nama detail akun: {ChartOfAccount.AccountName}.
                        Kode Detail Akun '{ChartOfAccount.AccountNo}' sudah digunakan. Pastikan anda menggunakan kode yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data kode akun dengan nama detail akun: {ChartOfAccount.AccountName}", ex);
                }
            }
        }

        public async Task UpdateChartOfAccount(ChartOfAccount ChartOfAccount)
        {
            if (ChartOfAccount.AccountHeaderName.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama Header Akun tidak boleh kosong!", null);

            if (ChartOfAccount.AccountName.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama Detail Akun tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.ChartOfAccounts.Update(ChartOfAccount);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data kode akun dengan nama detail akun: {ChartOfAccount.AccountName}.
                        Kode Detail Akun '{ChartOfAccount.AccountNo}' sudah digunakan. Pastikan anda menggunakan kode yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data kode akun dengan nama detail akun: {ChartOfAccount.AccountName}", ex);
                }
            }
        }

        public async Task DeleteChartOfAccount(ChartOfAccount ChartOfAccount)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.ChartOfAccounts.Remove(ChartOfAccount);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data kode akun dengan id: {ChartOfAccount.Id}", ex);
                }
            }
        }

    }
}
