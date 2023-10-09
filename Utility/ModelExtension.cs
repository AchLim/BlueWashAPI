using PurchaseAPI.Models;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Utility
{
    public static class ModelExtension
    {
        public static void PassValues(this BankAccount bankAccount, ref BankAccount receivedBankAccount)
        {
            receivedBankAccount.BankId = bankAccount.BankId;
            receivedBankAccount.AccountNumber = bankAccount.AccountNumber;
        }
        public static void PassValues(this Bank bank, ref Bank passedBank)
        {
            passedBank.Name = bank.Name;
        }

        public static void PassValues(this Currency currency, ref Currency receivedCurrency)
        {
            receivedCurrency.Name = currency.Name;
            receivedCurrency.Abbreviation = currency.Abbreviation;
        }

        public static void PassValues(this Product product, ref Product receivedProduct)
        {
            receivedProduct.Name = product.Name;
            receivedProduct.UnitOfMeasureId = product.UnitOfMeasureId;
        }

        public static void PassValues(this UnitOfMeasure uom, ref UnitOfMeasure passedUom)
        {
            passedUom.Name = uom.Name;
        }

        public static void PassValues(this Vendor vendor, ref Vendor passedVendor)
        {
            passedVendor.Name = vendor.Name;
            passedVendor.Reference = vendor.Reference;
            passedVendor.Address = vendor.Address;
            passedVendor.PhoneNumber = vendor.PhoneNumber;
            passedVendor.MobileNumber = vendor.MobileNumber;
            passedVendor.VendorEmail = vendor.VendorEmail;
            passedVendor.VendorSalesEmail = vendor.VendorSalesEmail;
            passedVendor.BankAccountId = vendor.BankAccountId;
            passedVendor.CurrencyId = vendor.CurrencyId;
            passedVendor.AccountPayable = vendor.AccountPayable;
        }

        public static void PassValues(this PurchaseOrder po, ref PurchaseOrder passedPo)
        {
            passedPo.Name = po.Name;
            passedPo.Date = po.Date;
            passedPo.VendorId = po.VendorId;
            passedPo.VendorReference = po.VendorReference;
            passedPo.TaxInclusive = po.TaxInclusive;
            passedPo.CurrencyId = po.CurrencyId;
            passedPo.TotalAmount = po.TotalAmount;
            passedPo.PurchaseOrderLines = po.PurchaseOrderLines;
        }

        public static bool IsNotEmpty<T>(this ICollection<T>? collections)
        {
            return collections != null && collections.Count > 0;
        }

        public static PurchaseOrder Convert(this PurchaseOrderDto purchaseOrderDto)
        {
            PurchaseOrder po = new()
            {
                Name = purchaseOrderDto.Name,
                VendorId = purchaseOrderDto.VendorId,
                VendorReference = purchaseOrderDto.VendorReference,
                Date = purchaseOrderDto.Date,
                CurrencyId = purchaseOrderDto.CurrencyId,
                TaxInclusive = purchaseOrderDto.TaxInclusive,
                TotalAmount = purchaseOrderDto.TotalAmount,
            };



            if (purchaseOrderDto.PurchaseOrderLines.IsNotEmpty())
            {
                po.PurchaseOrderLines = new List<PurchaseOrderLine>();
                foreach (var line in purchaseOrderDto.PurchaseOrderLines!)
                {
                    PurchaseOrderLine pol = new()
                    {
                        PurchaseOrderId = line.PurchaseOrderId,
                        ProductId = line.ProductId,
                        UnitOfMeasureId = line.UnitOfMeasureId,
                        Date = line.Date,
                        UnitPrice = line.UnitPrice,
                        Subtotal = line.Subtotal,
                    };

                    po.PurchaseOrderLines.Add(pol);
                }
            }

            return po;
        }
    }
}
