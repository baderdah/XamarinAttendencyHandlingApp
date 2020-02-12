using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abscence.Beans
{
    public class Course
    {
        [AutoIncrement, PrimaryKey]
        public int IdCourse { get; }
        public string CourseName { get; set; }
        public string Filiere { get; set; }
        public int IdTeacher { get; set; }
    }
}
