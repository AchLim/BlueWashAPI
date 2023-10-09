using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class PurchaseOrderMapper
    {
        public partial PurchaseOrderDto PurchaseOrderToPurchaseOrderDto(PurchaseOrder purchaseOrder);
        public partial PurchaseOrder PurchaseOrderDtoToPurchaseOrder(PurchaseOrderDto purchaseOrderDto);
    }

    [Mapper]
    public partial class PurchaseOrderLineMapper
    {
        public partial PurchaseOrderLineDto PurchaseOrderLineToPurchaseOrderLineDto(PurchaseOrderLine purchaseOrder);
        public partial PurchaseOrderLine PurchaseOrderLineDtoToPurchaseOrderLine(PurchaseOrderLineDto purchaseOrderDto);
    }
}
