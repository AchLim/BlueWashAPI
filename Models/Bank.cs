using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseAPI.Models
{
    public class Bank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 1)]

        [Display(Name = "Nama Bank")]
        public string Name { get; set; }
    }
}
