using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Index(nameof(Login), nameof(EmailAddress), IsUnique = true)]
    [Table("user")]
    public class ApplicationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(90, MinimumLength = 1)]
        public string Username { get; set; } = default!;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Login { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = default!;

        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Nomor Handphone tidak valid.")]
        [StringLength(30)]
        public string? MobileNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; } = default!;

        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }
}
