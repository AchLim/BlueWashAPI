using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class GeneralJournalHeaderMapper
    {
        public partial GeneralJournalHeaderDto GeneralJournalHeaderToGeneralJournalHeaderDto(GeneralJournalHeader chartOfAccount);
        public partial GeneralJournalHeader GeneralJournalHeaderDtoToGeneralJournalHeader(GeneralJournalHeaderDto chartOfAccountDto);
        public partial GeneralJournalHeader GeneralJournalHeaderUpdateDtoToGeneralJournalHeader(GeneralJournalHeaderUpdateDto chartOfAccountDto);

        private static DateTime DateTimeToDateTime(DateOnly dateOnly) => dateOnly.ToDateTime(TimeOnly.MinValue);
    }
}
