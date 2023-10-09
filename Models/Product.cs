using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PurchaseAPI.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "Nama")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Satuan")]
        [JsonIgnore]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }

        [Required]
        public int UnitOfMeasureId { get; set; }
    }
}
