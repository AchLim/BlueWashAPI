using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class VendorMapper
    {
        public partial VendorDto VendorToVendorDto(Vendor vendor);
        public partial Vendor VendorDtoToVendor(VendorDto vendorDto);
    }
}
