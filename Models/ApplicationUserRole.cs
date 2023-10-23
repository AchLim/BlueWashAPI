using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(ApplicationRoleId))]
    [Table("user_role")]
    public class ApplicationUserRole
    {
        public virtual ApplicationUser ApplicationUser { get; set; } = default!;
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; } = default!;
        public Guid ApplicationRoleId { get; set; }
    }
}
