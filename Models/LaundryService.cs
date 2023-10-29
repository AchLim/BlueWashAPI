using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Common;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    [Table("laundry_service")]
    [Index(nameof(Name), IsUnique = true)]
    public class LaundryService : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        [EnumDataType(typeof(LaundryProcess))]
        public LaundryProcess LaundryProcess { get; set; }
        
        // FK - One to many to Price Menu
        public virtual ICollection<PriceMenu>? PriceMenus { get; set; }

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    [Flags]
    public enum LaundryProcess : ushort
    {
        Wash = 1,
        Dry = 2,
        Iron = 4,
    }
}
