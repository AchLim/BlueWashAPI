using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class InventoryMapper
    {
        public partial InventoryDto InventoryToInventoryDto(Inventory currency);
        public partial Inventory InventoryDtoToInventory(InventoryDto currencyDto);
        public partial Inventory InventoryUpdateDtoToInventory(InventoryUpdateDto currencyDto);
    }
}
