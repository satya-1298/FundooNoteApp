using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public  UserEntity UserRegister(UserRegModel userRegModel);
        //public UserEntity UserLogin(UserLogModel userLogModel);
        public string UserLogin(LogUserModel log);
        public UserEntity GetUserByID(long UserId);

        public List<UserEntity> GetAllUsers();
        public string ForgotPassword(ForgetPassWordModel model);
        public bool ResetPassword(string email, ResetPassword resetPassword);


    }
}
