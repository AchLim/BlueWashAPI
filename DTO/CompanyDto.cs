namespace WebAPI.Models.DTO
{
    public class CompanyDto
    {
        public string Name { get; set; } = default!;
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
    }

    public class CompanyUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
    }
}
