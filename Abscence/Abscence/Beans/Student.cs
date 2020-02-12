using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abscence.Beans
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int IdStudent { get; set; }
        public int Cne {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
        public string Filiere { get; set; }
        public int IdTeacher { get; set; }
    }
}
