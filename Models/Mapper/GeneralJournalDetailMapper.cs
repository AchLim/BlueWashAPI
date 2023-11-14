using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class GeneralJournalDetailMapper
    {
        public partial GeneralJournalDetailDto GeneralJournalDetailToGeneralJournalDetailDto(GeneralJournalDetail generalJournalDetail);
        public partial GeneralJournalDetail GeneralJournalDetailDtoToGeneralJournalDetail(GeneralJournalDetailDto generalJournalDetailDto);
        public partial GeneralJournalDetail GeneralJournalDetailUpdateDtoToGeneralJournalDetail(GeneralJournalDetailUpdateDto generalJournalDetailUpdateDto);
    }
}
