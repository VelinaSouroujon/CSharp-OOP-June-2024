using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Pet : IWithName, IBirthable
    {
        public Pet(string name, DateTime birthdate)
        {
            Name = name;
            Birthdate = birthdate;
        }

        public string Name {  get; private set; }

        public DateTime Birthdate { get; private set; }
    }
}
