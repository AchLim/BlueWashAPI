using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("currency")]
    [Index(nameof(Name), new string[] { nameof(Code), nameof(CultureName) }, IsUnique = true)]
    public class Currency : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        [Display(Name = "Nama")]
        public string Name { get; set; } = default!;

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [Display(Name = "Kode")]
        public string Code { get; set; } = default!;

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Culture Name")]
        public string CultureName { get; set; } = default!;

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
