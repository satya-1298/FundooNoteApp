using CommonLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRepo:IUserRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
       

        //private readonly char[] key;

        public UserRepo(FundooContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext; 
            this.configuration = configuration;
        }
        public UserEntity  UserRegister(UserRegModel userRegModel)
        {
            try
            {
                
                UserEntity userEntity = new UserEntity();
               
                var result=fundooContext.User.FirstOrDefault(x=>x.Email == userRegModel.Email);
                if (result!=null)
                {
                    return null;
                }
                else
                {
                    userEntity.FirstName = userRegModel.FirstName;
                    userEntity.LastName = userRegModel.LastName;
                    userEntity.Email = userRegModel.Email;
                    userEntity.Password = userRegModel.Password;
                    fundooContext.User.Add(userEntity);
                    fundooContext.SaveChanges();
                    return userEntity;
                }
            }
            catch 
            {
                throw;
            }

        }
        // JWT TOKEN GENERATE:-
        public string GenerateToken(string Email, long UserId)
        {

            var tokenHandler =new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(configuration["JwtConfig:key"]);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", UserId.ToString()),
                    new Claim(ClaimTypes.Email, Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
            var token=tokenHandler.CreateToken(TokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public UserEntity GetUserByID(long UserId)
        {
            try
            {
                UserRegModel userRegModel = new UserRegModel();
                UserEntity valid = fundooContext.User.FirstOrDefault(x => x.UserId == (UserId));
                if (valid != null)
                {
                    return valid;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                throw;
            }
        }

        //public UserEntity UserLogin(UserLogModel userLogModel)
        //{
        //    try
        //    {
        //       UserEntity valid=fundooContext.User.FirstOrDefault(x=>x.Email==(userLogModel.Email) && x.Password==(userLogModel.Password));
        //        if(valid != null)
        //        {
        //            return valid;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
        public string UserLogin(LogUserModel log)
        {
            try
            {
                var userEntity = fundooContext.User.FirstOrDefault(u => u.Email == log.Email && u.Password == log.Password);


                if (userEntity != null)
                {
                    var jwtToken = GenerateToken(userEntity.Email, userEntity.UserId);
                    return jwtToken;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ;     
            }

        }
        public List<UserEntity> GetAllUsers()
        {
            try
            {
                var result = fundooContext.User.ToList();
                if (result.Count != 0)
                {
                    return result;
                }
                return null;

            }
            catch
            {
                return null;
            }
        }
        public string ForgotPassword(ForgetPassWordModel model)
        {
            try
            {
                var result = fundooContext.User.FirstOrDefault(u => u.Email == model.Email);
                if (result != null)
                {
                    var Token = GenerateToken(result.Email, result.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.SendData2Queue(Token);

                    return Token;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        public bool ResetPassword(string email, ResetPassword resetPassword)
        {
            try
            {
                if (resetPassword.newPassword.Equals(resetPassword.confirmPassword))
                {
                    var user = fundooContext.User.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = resetPassword.confirmPassword;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
