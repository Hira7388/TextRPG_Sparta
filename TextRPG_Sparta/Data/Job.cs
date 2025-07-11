using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Sparta.Data
{
    public class Job
    {
        public string name { get; set; }
        public string description { get; set; }

        public int jobBonusAtk { get; set; }
        public int jobBonusDef { get; set; }
        public int jobBonusHp { get; set; }
    }
}
