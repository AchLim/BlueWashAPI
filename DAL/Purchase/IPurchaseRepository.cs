using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<ItemPurchaseContainer>> GetTotalItemPurchaseData();
        Task<IEnumerable<PurchasePerInvoiceContainer>> GetTotalPurchasePerInvoiceData();
    }
}
