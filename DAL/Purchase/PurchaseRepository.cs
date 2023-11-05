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
