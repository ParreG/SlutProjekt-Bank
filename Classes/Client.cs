using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SlutProjekt_Bank.Classes
{
    public class Client(string password, string name, string surName, string email) 
        : User(password, name, surName, email)
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