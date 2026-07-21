namespace WebAPI.Models.DTO
{
    public class MessageTemplateDto
    {
        public string Name { get; set; } = default!;
        public string? Template { get; set; }
    }

    public class MessageTemplateUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Template { get; set; }
    }
}
