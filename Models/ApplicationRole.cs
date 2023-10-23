using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("role")]
    public class ApplicationRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; } = default!;

        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }
}
