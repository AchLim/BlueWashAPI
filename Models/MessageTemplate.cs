using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("message_template")]
    [Index(nameof(Name), IsUnique = true)]
    public class MessageTemplate : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nama Template tidak boleh kosong.")]
        public string Name { get; set; } = default!;

        [DataType(DataType.MultilineText)]
        public string? Template { get; set; } = default!;

        // Auditable
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
