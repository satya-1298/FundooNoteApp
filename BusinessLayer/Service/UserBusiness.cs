using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

       

        public UserEntity UserRegister(UserRegModel userRegModel)
        {
            try
            {
                return userRepo.UserRegister(userRegModel);
            }
            catch (Exception) 
            {
                throw;
            }
        }
        //public UserEntity UserLogin(UserLogModel userLogModel)
        //{
        //    try
        //    {
        //        return userRepo.UserLogin(userLogModel);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public string UserLogin(LogUserModel log)
        {
            try
            {
                return userRepo.UserLogin(log);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserEntity GetUserByID(long UserId)
        {
            try
            {
                return userRepo.GetUserByID(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserEntity> GetAllUsers()
        {
            try
            {
                return userRepo.GetAllUsers();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgotPassword(ForgetPassWordModel model)
        {
            try
            {
                return userRepo.ForgotPassword(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, ResetPassword resetPassword)
        {
            try
            {
                return userRepo.ResetPassword(email, resetPassword);
            }
            catch
            {
                throw;
            }
        }

    }
}
