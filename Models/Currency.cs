using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseAPI.Models
{
    public class Currency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Nama")]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Kode")]
        public string Abbreviation { get; set; }
    }
}
