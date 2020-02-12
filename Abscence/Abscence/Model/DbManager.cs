using Abscence.Beans;
using Abscence.persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Abscence.Model
{
    public class DbManager
    {
       

        public DbManager() { }
      
        public Teacher checkLoginTeacher(string login, string password, List<Teacher> teachers)
        {
            Teacher teacher = null;

            teacher = (from t in teachers
                    where t.UserName == login
                     && t.Password == password
                    select t).SingleOrDefault();


            return teacher;                
        }
    }
}
