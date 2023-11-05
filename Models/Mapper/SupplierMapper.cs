using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class SupplierMapper
    {
        public partial SupplierDto SupplierToSupplierDto(Supplier supplier);
        public partial Supplier SupplierDtoToSupplier(SupplierDto supplierDto);
        public partial Supplier SupplierUpdateDtoToSupplier(SupplierUpdateDto supplierDto);
    }
}
