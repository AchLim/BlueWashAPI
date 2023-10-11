using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class UnitOfMeasureMapper
    {
        [MapperIgnoreSource("Id")]
        public partial UnitOfMeasureDto UnitOfMeasureToUnitOfMeasureDto(UnitOfMeasure unitOfMeasure);


        [MapperIgnoreTarget("Id")]
        public partial UnitOfMeasure UnitOfMeasureDtoToUnitOfMeasure(UnitOfMeasureDto unitOfMeasureDto);
    }
}
