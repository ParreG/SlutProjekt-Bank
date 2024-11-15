using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Client(string password, string name, string surName, string email, string accountType) 
        : User(password, name, surName, email, accountType)
    {
        

        public void ChangePassword(User user, string newPassword)
        {
            user.Password = newPassword;
        }

        public void ChangeEmail(User user, string newEmail)
        {
            user.Email = newEmail;
        }
    }
}