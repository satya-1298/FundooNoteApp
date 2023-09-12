using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;  
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(LabelCreateModel model,long? NoteId)
        {
            var userId = long.Parse(User.FindFirst("UserId").Value);
            var result=labelBusiness.CreateLabel(model, NoteId, userId);
            if (result != null)
            {
                return Ok(new {message="Successfull",data=result} );
            }
            return BadRequest(new { message = "Unsuccessfull", data = result });
        }
        [Authorize]
        [HttpPost]
        [Route("NoteAdd/{nodeId}/{labelId}")]
        public IActionResult Add(long nodeId,long labelId)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId").Value);
                var result=labelBusiness.AddNote(nodeId, userId, labelId);
                if (result != null)
                {
                    return Ok(new { message = "Successfull",data=result });
                }
                else
                {
                    return BadRequest(new { message = "Unsuccessfull" });

                }
            }
            catch
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update/{newName}/{labelName}")]
        public IActionResult Update(string newName,string labelName)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("UserId").Value);
                var result = labelBusiness.UpdateLabel(newName, userId,labelName);
                if (result!=null)
                {
                    return Ok(new { message = "Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Unsuccessfull" });

                }
            }
            catch
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("LabelDelete/{labelName}")]
        public IActionResult Delete(string labelName)
        {
            try
            {
                var userId = long.Parse(User.FindFirst("userId").Value);
                var result = labelBusiness.DeleteLabel(labelName, userId);
                if (result!=null)
                {
                    return Ok(new { message = "Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { message = "Unsuccessfull" });

                }
            }
            catch
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Labels")]
        public IActionResult AllLabels()
        {
            var userId =long.Parse( User.FindFirst("UserId").Value);
            var result = labelBusiness.Retrieve(userId);
            if (result!=null)
            {
                return Ok(new { message = "Successfull", data = result });
            }
            else
            {
                return BadRequest(new { message = "Unsuccessfull" });

            }
        }
           
    }
}
