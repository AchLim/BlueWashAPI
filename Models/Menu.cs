using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Index(nameof(MenuCategoryId), nameof(MenuName), IsUnique = true)]
    [Table("menu")]
    public class Menu
    {
        public Guid Id { get; set; }

        public virtual MenuCategory MenuCategory { get; set; } = default!;
        public Guid MenuCategoryId { get; set; }

        public int MenuSequence { get; set; }
        public string MenuName { get; set; } = default!;

        public string MenuDisplayName { get; set; } = default!;
        public virtual ICollection<UserMenu>? UserMenus { get; set; }
    }
}
