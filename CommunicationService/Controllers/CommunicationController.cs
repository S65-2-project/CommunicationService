using System;
using System.Linq;
using System.Threading.Tasks;
using CommunicationService.Helper;
using CommunicationService.Exceptions;
using CommunicationService.Models;
using CommunicationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationController : ControllerBase
    {
        private readonly IChatService _chatService;

        public CommunicationController(IChatService chatService)
        {
            _chatService = chatService;
        }
        
        [HttpPost]
        public async Task<IActionResult> InitializeChat([FromBody]InitializeModel initmodel)
        {
            try
            {
                return Ok(await _chatService.InitializeChat(initmodel));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserChats(Guid id)
        {
            try
            {
                var chats = await _chatService.GetUserChats(id);
                return Ok(chats.WithoutMessages(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetChatMesssage(Guid id)
        {
            try
            {
                return Ok(await _chatService.GetChatMessages(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/{userId}")]
        public async Task<IActionResult> ReadChat(Guid id, Guid userId)
        {
            try
            {
                await _chatService.ReadChat(id, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("SendMessage/{id}")]
        public async Task<IActionResult> SendMessage(Guid id, [FromBody] MessageModel message)
        {
            try
            {
                return Ok(await _chatService.SendMessage(id, message));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}