using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abscence.Controllers
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public String Nom { get; set; }

        public override string ToString()
        {
            return Id + " " + Nom;
        }
    }
}
