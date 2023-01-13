using Microsoft.Data.SqlClient;
using Proje31.Models;
using System.Data;

namespace Proje31.Services
{
    public class AccountServiceImpl : AccountServices
    {
       

        private List<Account> accounts;

        

        public AccountServiceImpl()
        {
            accounts = new List<Account>
            {
                new Account
                {
                    username = "acc1",
                    pw = "123"
                }

            };
        }

        public Account Login(string username, string password)
        {
            
            return accounts.SingleOrDefault(a => a.username == username && a.pw == password);
        }
    }
}
