using System.Collections;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IMessageTemplateRepository
    {
        Task<IEnumerable<MessageTemplate>> GetAllMessageTemplates();
        Task<MessageTemplate?> GetMessageTemplateById(Guid id);
        Task InsertMessageTemplate(MessageTemplate messageTemplate);
        Task UpdateMessageTemplate(MessageTemplate messageTemplate);
        Task DeleteMessageTemplate(MessageTemplate messageTemplate);
    }
}