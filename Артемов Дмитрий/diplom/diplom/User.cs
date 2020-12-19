using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diplom
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }
        public string Subject { get; set; }
        public DateTime Time { get; set; }
        public int TestTry { get; set; } = 3;
        public int Result { get; set; }

    }
}
