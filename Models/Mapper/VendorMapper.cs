//using Riok.Mapperly.Abstractions;
//using WebAPI.Models.DTO;

//namespace WebAPI.Models.Mapper
//{
//    [Mapper]
//    public partial class VendorMapper
//    {
//        [MapperIgnoreSource("Id")]
//        [MapperIgnoreSource("BankAccount")]
//        [MapperIgnoreSource("Currency")]
//        public partial VendorDto VendorToVendorDto(Vendor vendor);


//        [MapperIgnoreTarget("Id")]
//        [MapperIgnoreTarget("BankAccount")]
//        [MapperIgnoreTarget("Currency")]
//        public partial Vendor VendorDtoToVendor(VendorDto vendorDto);


//        [MapperIgnoreTarget("BankAccount")]
//        [MapperIgnoreTarget("Currency")]
//        public partial Vendor VendorUpdateDtoToVendor(VendorUpdateDto vendorDto);
//    }
//}
