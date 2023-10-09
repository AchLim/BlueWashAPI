using Riok.Mapperly.Abstractions;
using PurchaseAPI.Models.DTO;

namespace PurchaseAPI.Models.Mapper
{
    [Mapper]
    public partial class UnitOfMeasureMapper
    {
        public partial UnitOfMeasureDto UnitOfMeasureToUnitOfMeasureDto(UnitOfMeasure unitOfMeasure);
        public partial UnitOfMeasure UnitOfMeasureDtoToUnitOfMeasure(UnitOfMeasureDto unitOfMeasureDto);
    }
}
