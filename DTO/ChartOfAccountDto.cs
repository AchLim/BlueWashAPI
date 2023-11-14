using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class ChartOfAccountDto
    {
        public int AccountHeaderNo { get; set; }
        public required string AccountHeaderName { get; set; }
        public int AccountNo { get; set; }
        public required string AccountName{ get; set; }
    }
    public class ChartOfAccountUpdateDto
    {
        public Guid Id { get; set; }
        public int AccountHeaderNo { get; set; }
        public required string AccountHeaderName { get; set; }
        public int AccountNo { get; set; }
        public required string AccountName { get; set; }
    }
}
