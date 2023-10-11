using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseAPI.Models
{
    public class Receipt
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nama")]
        public required string Name { get; set; }

        [Required]
        [Display(Name = "Pemasok")]
        public virtual Vendor Vendor { get; set; }

        [Required]
        public required int VendorId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal")]
        public DateTime Date { get; set; }

        [Display(Name = "Ref")]
        [StringLength(20)]
        public string? VendorReference { get; set; }

        [Display(Name = "Detail Struk")]
        public virtual ICollection<ReceiptLine>? ReceiptLines { get; set; }

        [Required]
        [Display(Name = "Mata Uang")]
        public virtual Currency Currency { get; set; }

        [Required]
        public required int CurrencyId { get; set; }

        [Display(Name = "Pajak Termasuk")]
        public bool TaxInclusive { get; set; }

        [Display(Name = "Tujuan Pembelian")]
        public string? PurposeOfPurchase { get; set; }

        [Display(Name = "Total")]
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
    }

    public class ReceiptLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        [Display(Name = "Pesanan Pembelian")]
        public virtual Receipt Receipt { get; set; }

        public required int ReceiptId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal")]
        public DateTime Date { get; set; }

        [Display(Name = "Deskripsi")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Barang")]
        [DeleteBehavior(DeleteBehavior.ClientSetNull)]
        public virtual Product Product { get; set; }
        [Required]
        public required int ProductId { get; set; }

        [Display(Name = "Kuantitas")]
        [Precision(18, 2)]
        public decimal Quantity { get; set; }

        [Required]
        [Display(Name = "Satuan")]
        [DeleteBehavior(DeleteBehavior.ClientSetNull)]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }

        [Required]
        public required int UnitOfMeasureId { get; set; }

        [Display(Name = "Harga Satuan")]
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Discount (%)")]
        [Precision(18, 5)]
        public decimal DiscountRate { get; set; }

        [Precision(18, 2)]
        public decimal Subtotal { get; set; }
    }
}
