using System;
using System.Collections.Generic;
using System.Text;

namespace Abscence.Controllers
{
   public class InputController
    {
        public string Msg { get; set;}

        public InputController(string login, string pass)
        {
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(pass))
            {
                Msg = "Not all fields are complete!!";
            }
        }


       public InputController(string login, string pass, string passConfirm) : this(login, pass)
        {         
            if(pass.Length <= 4)
            {
                Msg = "Password must contain at least 4 characters!!";
            }
            else if (!pass.Equals(passConfirm))
            {
                Msg = "Please confirm your password!!";
            }
        }

        public InputController(string cne, string lastName, string firstName, string mail, string tel)
        {
            if(String.IsNullOrWhiteSpace(cne) || String.IsNullOrWhiteSpace(lastName) || String.IsNullOrWhiteSpace(firstName)
                || String.IsNullOrWhiteSpace(mail) || String.IsNullOrWhiteSpace(tel))
            {
                Msg = "Not all fields are complete!!";
            }
        }
    }
}
