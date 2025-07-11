using System;
using TextRPG_Sparta.Scenes;

namespace TextRPG_Sparta.Data
{
    public class Player
    {
        public string name { get; private set; }
        public PlayerStat playerStat { get; private set; } = new PlayerStat();
        public Job chosenJob { get; private set; }
        public int gold = 0;
        public List<Item> inventory { get; private set; }
        public Item equippedArmor { get; private set; }
        public Item equippedWeapon { get; private set; }

        public Player(string name)
        {
            SetName(name);
            gold = 5000;
            inventory = new List<Item>();
        }
        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetJob(Job job)
        {
            chosenJob = job;
            if (job != null) // 직업이 null이 아닐 때만 스탯 적용
            {
                playerStat.AddToBaseAtk(job.jobBonusAtk);
                playerStat.AddToBaseDef(job.jobBonusDef);
                playerStat.AddToBaseHp(job.jobBonusHp);
            }
        }

        // 아이템 판매를 처리하는 메서드
        public void SellItem(Item item)
        {
            // 1. 만약 판매하려는 아이템이 장착 중이라면, 먼저 장착 해제
            if (equippedWeapon == item || equippedArmor == item)
            {
                Unequip(item);
            }

            // 2. 인벤토리에서 아이템 제거
            inventory.Remove(item);
        }

        public void EquipOrUnequipItem(Item item)
        {
            // 아이템 타입에 따라 맞는 슬롯에 장착/해제
            switch (item.Type)
            {
                case ItemType.Weapon:
                    // 이미 같은 아이템을 장착 중이면 -> 해제
                    if (equippedWeapon == item)
                    {
                        Unequip(item);
                    }
                    else // 다른 아이템을 장착 중이거나, 슬롯이 비어있으면 -> 장착
                    {
                        Equip(item);
                    }
                    break;
                case ItemType.Armor:
                    if (equippedArmor == item)
                    {
                        Unequip(item);
                    }
                    else
                    {
                        Equip(item);
                    }
                    break;
            }
        }

        // 장착 로직
        private void Equip(Item item)
        {
            // 이미 다른 아이템을 끼고 있었다면, 먼저 해제
            if (item.Type == ItemType.Weapon && equippedWeapon != null) Unequip(equippedWeapon);
            if (item.Type == ItemType.Armor && equippedArmor != null) Unequip(equippedArmor);

            // 새 아이템을 슬롯에 장착하고 스탯 적용
            if (item.Type == ItemType.Weapon) equippedWeapon = item;
            if (item.Type == ItemType.Armor) equippedArmor = item;

            playerStat.additionalAtk += item.BonusAtk;
            playerStat.additionalDef += item.BonusDef;
            playerStat.additionalMaxHealth += item.BonusHp;
        }

        // 해제 로직
        private void Unequip(Item item)
        {
            // 슬롯을 비우고 스탯 원상복구
            if (item.Type == ItemType.Weapon) equippedWeapon = null;
            if (item.Type == ItemType.Armor) equippedArmor = null;

            playerStat.additionalAtk -= item.BonusAtk;
            playerStat.additionalDef -= item.BonusDef;
            playerStat.additionalMaxHealth -= item.BonusHp;
        }
    }
}
