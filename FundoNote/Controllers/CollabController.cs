using CommonLayer.Model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabRepo _collabRepo;
        private readonly IBus bus;
        public CollabController(ICollabRepo collab,IBus bus)
        {
            this.bus = bus;
            this._collabRepo = collab;
        }
        [Authorize]
        [HttpPost]
        [Route("Create/{noteId}")]
        public IActionResult CreateCollab(CollabCreateModel collab, long noteId)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            string email=User.FindFirst(ClaimTypes.Email).Value.ToString();
            var result = _collabRepo.CreateCollab(collab, noteId, userId, email);
            if (result != null)
            {

                return Ok(new { message = "Successfull", data = result });
            }
            else
            {
                return BadRequest(new { message = "Email Already Exists" });
            }

        }
        [Authorize]
        [HttpGet]
        [Route("AllCollabs/{noteId}")]
        public IActionResult CollabsRecord(long noteId)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = _collabRepo.AllCollabs(noteId, userId);
            if (result != null)
            {

                return Ok(new { message = "Successfull", data = result });
            }
            else
            {
                return BadRequest(new { message = "Unsuccesful", data = result });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete/{collabId}")]
        public IActionResult DeleteCollab(long collabId)
        {
            try
            {
                _collabRepo.DeleteCollab(collabId);
                return Ok(new { message = "Successfull" });
            }
            catch
            {
                return BadRequest(new { message = "Unsuccesful" });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("EmailSend/{noteId}")]
        public async Task<IActionResult> SendEmail(CollabCreateModel model,long noteId)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            string email=User.FindFirst(ClaimTypes.Email).Value;    
            var result = _collabRepo.CreateCollab(model, noteId, userId, email);
            if(result != null)
            {
                Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(model);
                var mes = model.Email;
                return Ok(new {message="Successfully Send Message to Rabbit MQ",data=mes});
            }
            else
            {
                return BadRequest(new { message = "Unsuccesful" });
            }

        }

    }
}
