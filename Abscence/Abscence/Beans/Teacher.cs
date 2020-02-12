using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abscence.Beans
{
    public class Teacher
    {
        [PrimaryKey, AutoIncrement]
        public int IdTeacher { get; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
