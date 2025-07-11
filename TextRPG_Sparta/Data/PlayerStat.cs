using System;

namespace TextRPG_Sparta.Data
{
    public class PlayerStat
    {
        public int level;
        public int gold;

        // 기본 스텟
        public int baseAtk { get; private set; } = 10;
        public int baseDef { get; private set; } = 10;
        public int baseMaxHealth { get; private set; } = 100;

        // 추가 스텟
        public int additionalAtk = 0;
        public int additionalDef = 0;
        public int additionalMaxHealth = 0;

        // 총 스텟
        public int totalAtk => baseAtk + additionalAtk;
        public int totalDef => baseDef + additionalDef;
        public int totalHealth => baseMaxHealth + additionalMaxHealth;

        // current health
        public int currentHealth => baseMaxHealth;

        public void AddToBaseAtk(int atk)
        {
            baseAtk += atk;
        }
        public void AddToBaseDef(int def)
        {
            baseDef += def;
        }
        public void AddToBaseHp(int hp)
        {
            baseMaxHealth += hp;
        }
    }
}
