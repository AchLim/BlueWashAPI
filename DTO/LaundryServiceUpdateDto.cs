namespace WebAPI.DTO
{
    public class LaundryServiceDto
    {
        public required string Name { get; set; }
        public Boolean LaundryProcessWash { get; set; }
        public Boolean LaundryProcessDry { get; set; }
        public Boolean LaundryProcessIron { get; set; }
    }
    public class LaundryServiceUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Boolean LaundryProcessWash { get; set; }
        public Boolean LaundryProcessDry { get; set; }
        public Boolean LaundryProcessIron { get; set; }
    }
}
