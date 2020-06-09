using System;
using System.Threading.Tasks;
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