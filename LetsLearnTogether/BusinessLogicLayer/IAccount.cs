using LetsLearnTogether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsLearnTogether.BusinessLogicLayer
{
    public interface IAccount
    {
        string Register(string name, string email, string phone, string password, string role);
        User Login(string email, string password, string role);
    }
}
