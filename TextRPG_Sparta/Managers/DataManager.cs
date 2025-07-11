using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Sparta.Data;

namespace TextRPG_Sparta.Managers
{
    public class DataManager
    {
        private static DataManager _instance;
        public static DataManager Instance
        {
            get
            {
                if (_instance == null) _instance = new DataManager();
                return _instance;
            }
        }

        public List<Job> AllJobs { get; private set; }
        public Item[] AllItems { get; private set; }

        private DataManager()
        {
            AddJobs();
            AddItems();
        }

        private void AddItems()
        {
            AllItems = new Item[6];
            AllItems[0] = new Item { Name = "수련자의 갑옷", Description = "수련에 도움을 주는 갑옷입니다. ", Price = 1000, BonusDef = 5, Type = ItemType.Armor };
            AllItems[1] = new Item { Name = "무쇠갑옷", Description = "무쇠로 만들어져 튼튼한 갑옷입니다.", Price = 2000, BonusDef = 9, Type = ItemType.Armor };
            AllItems[2] = new Item { Name = "스파르타의 갑옷", Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", Price = 3500, BonusDef = 15 , Type = ItemType.Armor };
            AllItems[3] = new Item { Name = "낡은 검", Description = "쉽게 볼 수 있는 낡은 검 입니다.", Price = 600, BonusAtk = 8 , Type = ItemType.Weapon };
            AllItems[4] = new Item { Name = "청동 도끼", Description = "어디선가 사용됐던거 같은 도끼입니다.", Price = 1500, BonusAtk = 12, Type = ItemType.Weapon };
            AllItems[5] = new Item { Name = "스파르타의 창", Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.", Price = 3000, BonusAtk = 15, Type = ItemType.Weapon };
        }

        private void AddJobs()
        {
            AllJobs = new List<Job>();

            AllJobs.Add(new Job
            {
                name = "전사",
                description = "평균적인 지표를 가진 클래스입니다.",
                jobBonusAtk = 25,
                jobBonusDef = 50,
                jobBonusHp = 100
            });

            // "마법사" 직업 데이터 생성
            AllJobs.Add(new Job
            {
                name = "마법사",
                description = "강력한 마법을 사용하는 클래스입니다.",
                jobBonusAtk = 70,
                jobBonusDef = 45,
                jobBonusHp = 70
            });

            AllJobs.Add(new Job
            {
                name = "도적",
                description = "매우 강력한 공격을 가하지만 약한 몸을 가진 클래스입니다.",
                jobBonusAtk = 100,
                jobBonusDef = 35,
                jobBonusHp = 50
            });
        }
    }
}
