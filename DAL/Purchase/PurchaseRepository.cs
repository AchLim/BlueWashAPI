using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public sealed class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationContext _context;

        public PurchaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseHeader>> GetAllPurchaseHeaders()
        {
            IEnumerable<PurchaseHeader> purchaseHeaders = Enumerable.Empty<PurchaseHeader>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    purchaseHeaders = await _context.PurchaseHeaders
                                                            .Include(header => header.Supplier!)
                                                            .ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data pembelian.", ex);
                }
            }

            return purchaseHeaders;
        }

        public async Task<PurchaseHeader?> GetPurchaseHeaderById(Guid id)
        {
            PurchaseHeader? purchaseHeader = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    purchaseHeader = await _context.PurchaseHeaders
                                                            .Include(header => header.Supplier)
                                                            .Include(header => header.PurchaseDetails!)
                                                            .ThenInclude(detail => detail.Inventory)
                                                            .FirstOrDefaultAsync(header => header.Id == id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data pembelian dengan id: {id}", ex);
                }
            }

            return purchaseHeader;
        }

        public async Task InsertPurchaseHeader(PurchaseHeader purchaseHeader)
        {
            if (purchaseHeader.PurchaseNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Pembelian tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.PurchaseHeaders.AddAsync(purchaseHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data pembelian dengan nomor pembelian: {purchaseHeader.PurchaseNo}.
                        Nomor transaksi '{purchaseHeader.PurchaseNo}' sudah digunakan. Pastikan anda menggunakan nomor pembelian yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data pembelian dengan nomor transaksi: {purchaseHeader.PurchaseNo}", ex);
                }
            }
        }
        public async Task UpdatePurchaseHeader(PurchaseHeader purchaseHeader)
        {
            if (purchaseHeader.PurchaseNo.Trim() == string.Empty)
                throw new DatabaseInsertException("Nomor Transaksi tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.PurchaseHeaders.Update(purchaseHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (UniqueConstraintException ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUniqueConstraintException($@"
                        Terjadi kesalahan dalam memperbarui data pembelian dengan nomor pembelian: {purchaseHeader.PurchaseNo}.
                        Nomor pembelian '{purchaseHeader.PurchaseNo}' sudah digunakan. Pastikan anda menggunakan nomor pembelian yang unik.
                    ", ex);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data pembelian dengan nomor pembelian: {purchaseHeader.PurchaseNo}", ex);
                }
            }
        }
        public async Task DeletePurchaseHeader(PurchaseHeader purchaseHeader)
        {

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.PurchaseHeaders.Remove(purchaseHeader);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data pembelian dengan nomor pembelian: {purchaseHeader.PurchaseNo}", ex);
                }
            }
        }

        public async Task<IEnumerable<ItemPurchaseContainer>> GetTotalItemPurchaseData()
        {
            IEnumerable<ItemPurchaseContainer> itemPurchaseData = Enumerable.Empty<ItemPurchaseContainer>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    itemPurchaseData = await GetTotalItemPurchaseQuery().ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data pembelian barang.", ex);
                }
            }

            return itemPurchaseData;
        }

        public async Task<IEnumerable<PurchasePerInvoiceContainer>> GetTotalPurchasePerInvoiceData()
        {
            IEnumerable<PurchasePerInvoiceContainer> purchasePerInvoice = Enumerable.Empty<PurchasePerInvoiceContainer>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    purchasePerInvoice = await GetTotalPurchasePerInvoiceQuery().ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data pembelian per invoice.", ex);
                }
            }

            return purchasePerInvoice;
        }

        #region Queries

        private IQueryable<ItemPurchaseContainer> GetTotalItemPurchaseQuery()
        {
            return _context.Suppliers.Join(_context.PurchaseHeaders,
                                           supplier => supplier.Id,
                                           header => header.SupplierId,
                                           (supplier, header) => new
                                           {
                                               PurchaseId = header.Id,
                                               PurchaseNo = header.PurchaseNo,
                                               SupplierCode = supplier.SupplierCode
                                           })
                                     .Join(_context.PurchaseDetails,
                                           header => header.PurchaseId,
                                           detail => detail.PurchaseHeaderId,
                                           (header, detail) => new
                                           {
                                               PurchaseNo = header.PurchaseNo,
                                               InventoryId = detail.InventoryId,
                                               Quantity = detail.Quantity,
                                               Price = detail.Price,
                                               Subtotal = detail.Quantity * detail.Price
                                           })
                                     .Join(_context.Inventories,
                                           headerDetail => headerDetail.InventoryId,
                                           inventory => inventory.Id,
                                           (headerDetail, inventory) => new
                                           {
                                               PurchaseNo = headerDetail.PurchaseNo,
                                               ItemNo = inventory.ItemNo,
                                               Quantity = headerDetail.Quantity,
                                               Price = headerDetail.Price,
                                               Subtotal = headerDetail.Subtotal
                                           })
                                     .GroupBy(result => new
                                     {
                                         result.PurchaseNo,
                                         result.ItemNo,
                                         result.Quantity,
                                         result.Price,
                                         result.Subtotal
                                     })
                                     .Select(group => new ItemPurchaseContainer
                                     {
                                         PurchaseNo = group.Key.PurchaseNo,
                                         ItemNo = group.Key.ItemNo,
                                         Quantity = group.Key.Quantity,
                                         Price = group.Key.Price,
                                         Subtotal = group.Key.Subtotal
                                     });
        }

        private IQueryable<PurchasePerInvoiceContainer> GetTotalPurchasePerInvoiceQuery()
        {
            return _context.Suppliers.Join(GetTotalItemPurchaseQuery().Join(_context.PurchaseHeaders,
                                                                            itemPurchase => itemPurchase.PurchaseNo,
                                                                            header => header.PurchaseNo,
                                                                            (itemPurchase, header) => new
                                                                            {
                                                                                PurchaseNo = header.PurchaseNo,
                                                                                PurchaseDate = header.PurchaseDate,
                                                                                SupplierId = header.SupplierId,
                                                                                Subtotal = itemPurchase.Subtotal
                                                                            }),
                                           supplier => supplier.Id,
                                           itemPurchaseHeader => itemPurchaseHeader.SupplierId,
                                           (supplier, itemPurchaseHeader) => new
                                           {
                                               PurchaseNo = itemPurchaseHeader.PurchaseNo,
                                               PurchaseDate = itemPurchaseHeader.PurchaseDate,
                                               SupplierCode = supplier.SupplierCode,
                                               SupplierName = supplier.SupplierName,
                                               Subtotal = itemPurchaseHeader.Subtotal
                                           })
                                           .GroupBy(result => new
                                           {
                                               result.PurchaseNo,
                                               result.PurchaseDate,
                                               result.SupplierCode,
                                               result.SupplierName,
                                           })
                                           .OrderBy(group => group.Key.PurchaseNo)
                                           .Select(group => new PurchasePerInvoiceContainer
                                           {
                                               PurchaseNo = group.Key.PurchaseNo,
                                               PurchaseDate = group.Key.PurchaseDate,
                                               SupplierCode = group.Key.SupplierCode,
                                               SupplierName = group.Key.SupplierName,
                                               Subtotal = group.Sum(item => item.Subtotal),
                                           });
        }
        #endregion
    }
    public class ItemPurchaseContainer
    {
        public string PurchaseNo { get; set; } = default!;
        public string ItemNo { get; set; } = default!;

        [Precision(19, 3)]
        public decimal Quantity { get; set; }

        [Precision(19, 3)]
        public decimal Price { get; set; }

        [Precision(19, 3)]
        public decimal Subtotal { get; set; }
    }
    public class PurchasePerInvoiceContainer
    {
        public string PurchaseNo { get; set; } = default!;
        public DateOnly PurchaseDate { get; set; }
        public string SupplierCode { get; set; } = default!;
        public string SupplierName { get; set; } = default!;

        [Precision(19, 3)]
        public decimal Subtotal { get; set; }
    }
}
