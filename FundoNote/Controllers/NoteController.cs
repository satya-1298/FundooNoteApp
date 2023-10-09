using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness business;
        private readonly IDistributedCache distributedCache;

        public NoteController(INoteBusiness noteBusiness, IDistributedCache distributedCache)

        {
            this.distributedCache = distributedCache;
            this.business = noteBusiness;
        }
        [Authorize]
        [HttpPost]
        [Route("CreateNotes")]
        public IActionResult NotesCreation(NoteCreateModel model)
        {
            try
            {
                long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = business.CreateNote(model, userId);
                if (result != null)
                {
                    return this.Ok(new { message = "Data Entered Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { message = "UnSuccess", data = result });
                }

            }
            catch
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateNote/{NoteID}")]
        public IActionResult NoteUpdate(NoteCreateModel noteUpdate, long NoteID)
        {
            try
            {
                long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = business.UpdateNote(noteUpdate, NoteID, userId);
                if (result != null)
                {
                    return Ok(new { message = "Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Unsuccessfull", data = result });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllNote")]
        public IActionResult GetNotes()
        {
            try
            {
                long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = business.GetAllNotes(userId);
                if (result != null)
                {
                    return Ok(new { message = "Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Unsuccessfull", data = userId });
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteNote/{noteID}")]
        public IActionResult DeleteNotes(int noteID)
        {
            try
            {
                long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = business.DeleteNote(noteID, userId);
                if (result!=null)
                {
                    return Ok(new { message = "Successfull"});
                }
                else
                {
                    return BadRequest(new { message = "unSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("IsArchive/{noteID}")]
        public IActionResult ArchieveNote(long noteID)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = business.IsArchiev(noteID);
            if (result !=null)
            {
                return Ok(new { message = "Successfull" ,data = result });
            }
            else
            {
                return BadRequest(new { message = "unSuccessfull" });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("IsPin/{noteID}")]
        public IActionResult PinNote(long noteID)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = business.IsPin(noteID);
            if (result !=null)
            {
                return Ok(new { message = "Successfull" });
            }
            else
            {
                return BadRequest(new { message = "unSuccessfull" });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("IsTrash/{noteID}")]
        public IActionResult NoteTrash(long noteID)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = business.IsTrash(noteID);
            if (result!=null)
            {
                return Ok(new { message = "Successfull" , data = result });
            }
            else
            {
                return BadRequest(new { message = "unSuccessfull" });
            }

        }
       
        [Authorize]
        [HttpGet("Search/{word}")]
        public IActionResult SearchNote(string word)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = business.SearchQuery(userId, word);
            if (result != null)
            {
                return Ok(new { message = "Searched Note Successfully", data = result });
            }
            else
            {
                return BadRequest(new { message = "unSuccessfull" });
            }
        }



        [Authorize]
        [HttpPost]
        [Route("ImageUpload/{id}")]
        public async Task<IActionResult> AddImage(long id, IFormFile imageFile)
        {

            // var userId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            long userId = long.Parse(User.FindFirst("UserId").Value);
            Tuple<int, string> result = await business.Image(id, userId, imageFile);
            if (result.Item1 == 1)
            {
                return Ok(new { success = true, messege = "Image Update  Sucessfully", data = result });
            }
            else
            {
                return NotFound(new { success = false, messege = "Image Update  Unucessfully", data = result });
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("AllNotesRedis")]
        public async Task<IActionResult> GetAllByRedis()
        {
            try
            {
                var cacheKey = "NotesList";
                string serializedNotesList;
                var NoteList = new List<NotesEntity>();
                var redisNotesList = await distributedCache.GetAsync(cacheKey);
                if (redisNotesList != null)
                {
                    serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                    NoteList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
                }
                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                    var userId = Int32.Parse(userid.Value);
                    NoteList = business.GetAllNotes(userId);
                    serializedNotesList = JsonConvert.SerializeObject(NoteList);
                    redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisNotesList, options);
                }
                return Ok(new { success = true, message = "Redis User Found Successfully", data = NoteList });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("CopyNote/{noteId}")]
        public IActionResult NoteCopy(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("UserId").Value);

                var result = business.CopyNote(userId, noteId);
                if (result != null)
                {
                    return Ok(new { message = "Note Copied Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Unsuccessfull", data = result });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }


}


