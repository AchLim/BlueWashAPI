namespace WebAPI.Models.DTO
{
    public class BankDto
    {
        public required string Name { get; set; }
    }

    public class BankUpdateDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
