using LetsLearnTogether.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LetsLearnTogether.DataLogicLayer
{
    public class AccountRepository : IAccountRepository
    {

        public string Register(string name, string email, string phone, string password, string role)
        {
            using(var dBContext = new DBContext())
            {
                if(dBContext.User.Any(a => a.Email.Equals(email)))
                    return "Email already Exists !";

                User user = new User();
                user.Name = name;
                user.Email = email;
                user.PhoneNumber = phone;
                user.Password = password;
                user.Role = role;

                var enntry = dBContext.Entry(user);
                enntry.State = EntityState.Added;
                int isAdded = dBContext.SaveChanges();
                if (isAdded == 1)
                    return "true";
            }
            return "something went wrong";
        }

        public User Login(string email, string password, string role)
        {
            User user = new User();
            using(var  dbContext = new DBContext())
            {
                user = dbContext.User.Where(a => a.Email.Equals(email) && a.Password.Equals(password) && a.Role == role).FirstOrDefault();
            }
            return user;
        }
    }
}