using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public static partial class JournalEntryMapper
    {
        public static partial JournalEntryDto JournalEntryToJournalEntryDto(JournalEntry journalEntry);
        public static partial JournalEntry JournalEntryDtoToJournalEntry(JournalEntryDto journalEntryDto);
        public static partial JournalEntry JournalEntryUpdateDtoToJournalEntry(JournalEntryUpdateDto journalEntryUpdateDto);
    }
}
