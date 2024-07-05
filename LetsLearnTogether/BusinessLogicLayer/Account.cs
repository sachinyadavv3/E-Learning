using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LetsLearnTogether.DataLogicLayer;
using LetsLearnTogether.Models;

namespace LetsLearnTogether.BusinessLogicLayer
{
    public class Account : IAccount
    {
        private readonly IAccountRepository _accountRepository;
        public Account()
        {
            _accountRepository = new AccountRepository();
        }

        public string Register(string name, string email, string phone, string password, string role)
        {
            string msg = _accountRepository.Register(name, email, phone, password, role);
            return msg;
        }

        public User Login(string email, string password, string role)
        {
            return _accountRepository.Login(email, password, role);
        }
    }
}