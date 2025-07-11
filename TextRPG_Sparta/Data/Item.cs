using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Sparta.Data
{ 
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
        public int Price { get; set; }

        // 아이템이 제공하는 능력치
        public int BonusAtk { get; set; }
        public int BonusDef { get; set; }
        public int BonusHp { get; set; }

        // 여기에 아이템의 다른 속성들을 추가할 수 있습니다.
    }
}
