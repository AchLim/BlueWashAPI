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
        public static void PassData(this MessageTemplateUpdateDto dto, ref MessageTemplate template)
        {
            template.Name = dto.Name;
            template.Template = dto.Template;
        }
        public static void PassData(this CompanyUpdateDto dto, ref Company company)
        {
            company.Name = dto.Name;
            company.Address = dto.Address;
            company.MobileNumber = dto.MobileNumber;
        }
        public static void PassData(this CustomerUpdateDto dto, ref Customer customer)
        {
            customer.CustomerName = dto.CustomerName;
            customer.CustomerCode = dto.CustomerCode;
            customer.CustomerAddress = dto.CustomerAddress;
            customer.MobileNumber = dto.MobileNumber;
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
            journalEntry.Status = dto.Status;
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
            purchaseHeader.PaymentTerm = dto.PaymentTerm;
            purchaseHeader.Status = dto.Status;
            purchaseHeader.PaymentStatus = dto.PaymentStatus;

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
                        Price = detail.Price,
                        Discount = detail.Discount,
                    });
                }
            }

            purchaseHeader.PurchaseDetails = purchaseDetails;

            List<PurchasePayment> purchasePayments = new();

            if (dto.PurchasePayments.IsNotEmpty())
            {
                foreach (var payment in dto.PurchasePayments!)
                {
                    purchasePayments.Add(new PurchasePayment
                    {
                        PurchasePaymentId = payment.PurchasePaymentId,
                        PaymentDate = payment.PaymentDate,
                        Type = payment.Type,
                        ReferenceNumber = payment.ReferenceNumber,
                        Amount = payment.Amount
                    });
                }
            }

            purchaseHeader.PurchasePayments = purchasePayments;
        }

        public static void PassData(this SalesHeaderUpdateDto dto, ref SalesHeader salesHeader)
        {
            salesHeader.SalesNo = dto.SalesNo;
            salesHeader.SalesDate = dto.SalesDate;
            salesHeader.CustomerId = dto.CustomerId;
            salesHeader.Description = dto.Description;
            salesHeader.PaymentTerm = dto.PaymentTerm;
            salesHeader.Status = dto.Status;
            salesHeader.PaymentStatus = dto.PaymentStatus;

            List<SalesDetail> salesDetails = new();

            if (dto.SalesDetails.IsNotEmpty())
            {
                foreach (var detail in dto.SalesDetails!)
                {
                    salesDetails.Add(new SalesDetail
                    {
                        SalesDetailId = detail.SalesDetailId,
                        LaundryServiceId = detail.LaundryServiceId,
                        PriceMenuId = detail.PriceMenuId,
                        Quantity = detail.Quantity,
                        Price = detail.Price,
                        Discount = detail.Discount,
                    });
                }
            }

            salesHeader.SalesDetails = salesDetails;

            List<SalesPayment> salesPayments = new();

            if (dto.SalesPayments.IsNotEmpty())
            {
                foreach (var payment in dto.SalesPayments!)
                {
                    salesPayments.Add(new SalesPayment
                    {
                        SalesPaymentId = payment.SalesPaymentId,
                        PaymentDate = payment.PaymentDate,
                        Type = payment.Type,
                        ReferenceNumber = payment.ReferenceNumber,
                        Amount = payment.Amount
                    });
                }
            }

            salesHeader.SalesPayments = salesPayments;
        }

        public static void PassData(this LaundryServiceUpdateDto dto, ref LaundryService laundryService)
        {
            laundryService.Name = dto.Name;
            laundryService.LaundryProcess = dto.LaundryProcess;

            List<PriceMenu> priceMenus = new();

            if (dto.PriceMenus.IsNotEmpty())
            {
                foreach (var menu in dto.PriceMenus!)
                {
                    priceMenus.Add(new PriceMenu
                    {
                        PriceMenuId = menu.PriceMenuId,
                        Name = menu.Name,
                        Price = menu.Price,
                        PricingOption = menu.PricingOption,
                        DeliveryOption = menu.DeliveryOption,
                        ProcessingTime = menu.ProcessingTime,
                        TimeUnit = menu.TimeUnit
                    });
                }
            }

            laundryService.PriceMenus = priceMenus;
        }


        public static bool IsNotEmpty<T>(this ICollection<T>? collections)
        {
            return collections != null && collections.Count > 0;
        }
    }
}
