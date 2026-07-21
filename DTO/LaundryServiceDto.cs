namespace WebAPI.Models.DTO
{
    public class LaundryServiceDto
    {
        public required string Name { get; set; }
        public ushort LaundryProcess { get; set; }
        public ICollection<PriceMenuDto>? PriceMenus { get; set; }
    }
    public class LaundryServiceUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ushort LaundryProcess { get; set; }
        public ICollection<PriceMenuDto>? PriceMenus { get; set; }
    }
}
