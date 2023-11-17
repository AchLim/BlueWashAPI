using WebAPI.DTO;
using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Utility
{
    public static class ModelExtension
    {
        public static void PassData(this CurrencyUpdateDto dto, ref Currency currency)
        {
            currency.Name = dto.Name;
            currency.Code = dto.Code;
            currency.CultureName = dto.CultureName;
        }
        public static void PassData(this InventoryUpdateDto dto, ref Inventory inventory)
        {
            inventory.ItemNo = dto.ItemNo;
            inventory.ItemName = dto.ItemName;
        }

        public static void PassData(this LaundryServiceUpdateDto dto, ref LaundryService laundryService)
        {
            laundryService.Name = dto.Name;

            var result = 0;

            if (dto.LaundryProcessWash)
                result |= (ushort)LaundryProcess.Wash;

            if (dto.LaundryProcessDry)
                result |= (ushort)LaundryProcess.Dry;

            if (dto.LaundryProcessIron)
                result |= (ushort)LaundryProcess.Iron;

            laundryService.LaundryProcess = (LaundryProcess)result;
        }
        public static void PassData(this CustomerUpdateDto dto, ref Customer customer)
        {
            customer.CustomerName = dto.CustomerName;
            customer.CustomerCode = dto.CustomerCode;
            customer.CustomerAddress = dto.CustomerAddress;
            customer.CurrencyId = dto.CurrencyId;
        }
        public static void PassData(this SupplierUpdateDto dto, ref Supplier supplier)
        {
            supplier.SupplierName = dto.SupplierName;
            supplier.SupplierCode = dto.SupplierCode;
            supplier.SupplierAddress = dto.SupplierAddress;
            supplier.CurrencyId = dto.CurrencyId;
        }

        public static void PassData(this ChartOfAccountUpdateDto dto, ref ChartOfAccount chartOfAccount)
        {
            chartOfAccount.AccountHeaderNo = dto.AccountHeaderNo;
            chartOfAccount.AccountHeaderName = dto.AccountHeaderName;
            chartOfAccount.AccountNo = dto.AccountNo;
            chartOfAccount.AccountName = dto.AccountName;
        }

        public static void PassData(this JournalEntryUpdateDto dto, ref JournalEntry journalEntry)
        {
            journalEntry.TransactionNo = dto.TransactionNo;
            journalEntry.TransactionDate = dto.TransactionDate;
            journalEntry.Description = dto.Description;

            List<JournalItem> journalItems = new();

            if (dto.JournalItems.IsNotEmpty())
            {
                foreach (var detail in dto.JournalItems!)
                {
                    journalItems.Add(new JournalItem
                    { 
                        JournalItemId = detail.JournalItemId,
                        ChartOfAccountId = detail.ChartOfAccountId,
                        Debit = detail.Debit,
                        Credit = detail.Credit
                    });
                }
            }

            journalEntry.JournalItems = journalItems;
        }

        public static void PassData(this PurchaseHeaderUpdateDto dto, ref PurchaseHeader purchaseHeader)
        {
            purchaseHeader.PurchaseNo = dto.PurchaseNo;
            purchaseHeader.PurchaseDate = dto.PurchaseDate;
            purchaseHeader.SupplierId = dto.SupplierId;
            purchaseHeader.Description = dto.Description;

            List<PurchaseDetail> purchaseDetails = new();

            if (dto.PurchaseDetails.IsNotEmpty())
            {
                foreach (var detail in dto.PurchaseDetails!)
                {
                    purchaseDetails.Add(new PurchaseDetail
                    {
                        PurchaseDetailId = detail.PurchaseDetailId,
                        InventoryId = detail.InventoryId,
                        Quantity = detail.Quantity,
                        Price = detail.Price
                    });
                }
            }

            purchaseHeader.PurchaseDetails = purchaseDetails;
        }


        public static bool IsNotEmpty<T>(this ICollection<T>? collections)
        {
            return collections != null && collections.Count > 0;
        }
    }
}
