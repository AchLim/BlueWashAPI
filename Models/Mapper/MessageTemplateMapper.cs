using Riok.Mapperly.Abstractions;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Mapper
{
    [Mapper]
    public partial class MessageTemplateMapper
    {
        public partial MessageTemplateDto MessageTemplateToMessageTemplateDto(MessageTemplate customer);
        public partial MessageTemplate MessageTemplateDtoToMessageTemplate(MessageTemplateDto customerDto);
        public partial MessageTemplate MessageTemplateUpdateDtoToMessageTemplate(MessageTemplateUpdateDto customerDto);
    }
}
