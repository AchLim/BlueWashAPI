using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PurchaseAPI.Models
{
    public class BankAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual Bank Bank { get; set; }

        [Required]
        public int BankId { get; set; }


        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Nomor Rekening")]
        public string AccountNumber { get; set; }
    }
}




