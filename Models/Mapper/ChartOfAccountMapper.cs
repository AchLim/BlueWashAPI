using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class ChartOfAccountMapper
    {
        public partial ChartOfAccountDto ChartOfAccountToChartOfAccountDto(ChartOfAccount chartOfAccount);
        public partial ChartOfAccount ChartOfAccountDtoToChartOfAccount(ChartOfAccountDto chartOfAccountDto);
        public partial ChartOfAccount ChartOfAccountUpdateDtoToChartOfAccount(ChartOfAccountUpdateDto chartOfAccountDto);
    }
}
