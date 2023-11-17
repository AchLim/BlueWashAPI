using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<PurchaseHeader>> GetAllPurchaseHeaders();
        Task<PurchaseHeader?> GetPurchaseHeaderById(Guid id);
        Task InsertPurchaseHeader(PurchaseHeader purchaseHeader);
        Task UpdatePurchaseHeader(PurchaseHeader purchaseHeader);
        Task DeletePurchaseHeader(PurchaseHeader purchaseHeader);

        Task<IEnumerable<ItemPurchaseContainer>> GetTotalItemPurchaseData();
        Task<IEnumerable<PurchasePerInvoiceContainer>> GetTotalPurchasePerInvoiceData();
    }
}
