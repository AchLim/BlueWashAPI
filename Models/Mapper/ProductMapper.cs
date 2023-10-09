using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class ProductMapper
    {
        public partial ProductDto ProductToProductDto(Product product);
        public partial Product ProductDtoToProduct(ProductDto productDto);
    }
}
