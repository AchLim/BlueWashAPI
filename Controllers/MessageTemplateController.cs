using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Mapper;
using WebAPI.Utility;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN,MANAGER,STAFF")]
    [Route("message-template")]
    public class MessageTemplateController : ControllerBase
    {
        private readonly ILogger<MessageTemplateController> _logger;
        private readonly IMessageTemplateRepository _messageTemplateRepository;

        public MessageTemplateController(ILogger<MessageTemplateController> logger, IMessageTemplateRepository messageTemplateRepository)
        {
            _logger = logger;
            _messageTemplateRepository = messageTemplateRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<MessageTemplate>> GetAllMessageTemplates()
        {
            IEnumerable<MessageTemplate> messageTemplates = await _messageTemplateRepository.GetAllMessageTemplates();
            return Ok(messageTemplates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MessageTemplate>> GetMessageTemplateById(Guid id)
        {
            MessageTemplate? messageTemplate = await _messageTemplateRepository.GetMessageTemplateById(id);
            return Ok(messageTemplate);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<MessageTemplate>> PostMessageTemplate([FromBody] MessageTemplateDto messageTemplateDto)
        {
            MessageTemplateMapper messageTemplateMapper = new();
            MessageTemplate messageTemplate = messageTemplateMapper.MessageTemplateDtoToMessageTemplate(messageTemplateDto);
            await _messageTemplateRepository.InsertMessageTemplate(messageTemplate);

            return CreatedAtAction(nameof(GetMessageTemplateById), new { id = messageTemplate.Id }, messageTemplate);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<MessageTemplate>> UpdateMessageTemplate(Guid id, [FromBody] MessageTemplateUpdateDto messageTemplateDto)
        {
            if (id != messageTemplateDto.Id)
                return BadRequest("ID template WhatsApp tidak cocok!");

            MessageTemplate? messageTemplate = await _messageTemplateRepository.GetMessageTemplateById(id);
            if (messageTemplate is null)
                return BadRequest($"Template WhatsApp dengan id: {id} tidak ditemukan");

            messageTemplateDto.PassData(ref messageTemplate);
            await _messageTemplateRepository.UpdateMessageTemplate(messageTemplate);

            return CreatedAtAction(nameof(GetMessageTemplateById), new { id = messageTemplate.Id }, messageTemplate);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteMessageTemplate(Guid id)
        {
            MessageTemplate? messageTemplate = await _messageTemplateRepository.GetMessageTemplateById(id);
            if (messageTemplate is null)
                return BadRequest($"Data template WhatsApp dengan id: {id} tidak ditemukan!");

            await _messageTemplateRepository.DeleteMessageTemplate(messageTemplate);

            return Ok();
        }
    }
}