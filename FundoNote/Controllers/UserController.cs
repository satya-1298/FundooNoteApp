using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)

        {
            this.userBusiness = userBusiness;   
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult UserRegister(UserRegModel userRegModel )
        {
            try
            {
                var result = userBusiness.UserRegister(userRegModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration UnSuccessfull/User with Email Exists" });
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }
        //[HttpPost]
        //[Route("Login")]
        //public IActionResult UserLogin(UserLogModel userLogModel)
        //{
        //    try
        //    {
        //        var result = userBusiness.UserLogin(userLogModel);
        //        if (result != null)
        //        {
        //            return this.Ok(new { success = true, message = "Login Successfull", data = result });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { success = false, message = "Login UnSuccessfull" });
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(LogUserModel log)
        {
            var result = userBusiness.UserLogin(log);
            if (result != null)
            {
                return Ok(new { success = true, message = "User Login Successful", token = result });
            }
            else
            {
                return NotFound(new { success = false, message = "User Login Failed", token = result });

            }
        }
       
       
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgetPassWordModel model)
        {
            var result = userBusiness.ForgotPassword(model);
            if (result != null)
            {
                return Ok(new { success = true, message = "Forgot Pass Email Send Successfully" });
            }
            else
            {
                return NotFound(new { success = false, message = "Forgot pass email not send..." });
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("ResetPass")]
        public IActionResult ResetPassword(ResetPassword resetPassword)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
            var result = userBusiness.ResetPassword(email, resetPassword);
            if (result != null)
            {
                return Ok(new { success = true, message = "Password Changed Successfully", data = result });
            }
            else
            {
                return NotFound(new { success = false, message = "Password not changed", data = result });
            }
        }

    }
}
