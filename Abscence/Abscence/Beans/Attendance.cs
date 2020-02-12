using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abscence.Beans
{
    public class Attendance
    {
        [PrimaryKey, AutoIncrement]
        public int IdAbscence { get;}

        public string Day { get; set; }
        public string Start { get; set; }
        public string End { get; set; } 
        public int IdStudent { get; set; }
        public string Course { get; set; }

    }
}
