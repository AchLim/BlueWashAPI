using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;

namespace WebAPI.DAL
{
    public sealed class MessageTemplateRepository : IMessageTemplateRepository
    {
        private readonly ApplicationContext _context;

        public MessageTemplateRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MessageTemplate>> GetAllMessageTemplates()
        {
            IEnumerable<MessageTemplate> companies = Enumerable.Empty<MessageTemplate>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    companies = await _context.MessageTemplates.ToListAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException("Terjadi kesalahan dalam pengambilan data template WhatsApp", ex);
                }
            }

            return companies;
        }

        public async Task<MessageTemplate?> GetMessageTemplateById(Guid id)
        {
            MessageTemplate? messageTemplate = null;
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    messageTemplate = await _context.MessageTemplates.FindAsync(id);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseReadException($"Terjadi kesalahan dalam pengambilan data template WhatsApp dengan id: {id}", ex);
                }
            }

            return messageTemplate;
        }

        public async Task InsertMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate.Name.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama template tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.MessageTemplates.AddAsync(messageTemplate);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseInsertException($"Terjadi kesalahan dalam menambahkan data template WhatsApp dengan nama: {messageTemplate.Name}", ex);
                }
            }
        }

        public async Task UpdateMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate.Name.Trim() == string.Empty)
                throw new DatabaseInsertException("Nama perusahaan tidak boleh kosong!", null);

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.MessageTemplates.Update(messageTemplate);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseUpdateException($"Terjadi kesalahan dalam memperbarui data template WhatsApp dengan nama: {messageTemplate.Name}", ex);
                }
            }
        }

        public async Task DeleteMessageTemplate(MessageTemplate messageTemplate)
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.MessageTemplates.Remove(messageTemplate);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DatabaseDeleteException($"Terjadi kesalahan dalam menghapus data template WhatsApp dengan id: {messageTemplate.Id}", ex);
                }
            }
        }
    }
}
