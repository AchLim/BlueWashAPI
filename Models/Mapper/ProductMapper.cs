using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class ProductMapper
    {
        [MapperIgnoreSource("Id")]
        [MapperIgnoreSource("UnitOfMeasure")]
        public partial ProductDto ProductToProductDto(Product product);

        [MapperIgnoreTarget("Id")]
        [MapperIgnoreTarget("UnitOfMeasure")]
        public partial Product ProductDtoToProduct(ProductDto productDto);

        [MapperIgnoreTarget("UnitOfMeasure")]
        public partial Product ProductUpdateDtoToProduct(ProductUpdateDto productDto);
    }
}
