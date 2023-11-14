using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("menu_category")]
    [Index(nameof(CategoryName), IsUnique = true)]
    public class MenuCategory
    {
        public Guid Id { get; set; }

        public int CategorySequence { get; set; }
        public string CategoryName { get; set; } = default!;
        public string CategoryDisplayName { get; set; } = default!;

        public virtual ICollection<Menu>? Menus { get; set; }
    }
}
