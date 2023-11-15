using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class JournalItemMapper
    {
        public partial JournalItemDto JournalItemToJournalItemDto(JournalItem journalItem);
        public partial JournalItem JournalItemDtoToJournalItem(JournalItemDto journalItemDto);
        public partial JournalItem JournalItemUpdateDtoToJournalItem(JournalItemUpdateDto journalItemUpdateDto);
    }
}
