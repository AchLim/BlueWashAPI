using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(MenuId))]
    [Table("user_menu")]
    public class UserMenu
    {
        public virtual ApplicationUser ApplicationUser { get; set; } = default!;
        public Guid ApplicationUserId { get; set; }

        public Menu Menu { get; set; } = default!;
        public Guid MenuId { get; set; } = default!;
    }
}
