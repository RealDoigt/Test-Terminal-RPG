using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_RPG
{
    abstract class GameCharacter
    {
        public byte ATK { get; set; }
        public byte HP { get; set; }
        public byte DEF { get; set; }
    }
}
