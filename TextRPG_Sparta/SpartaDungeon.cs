
using TextRPG_Sparta.Data;
using TextRPG_Sparta.Managers;

namespace TextRPG_Sparta
{
    class SpartaDungeon
    {
        static void Main(string[] args)
        {
            GameManager.Instance.GameLoop();
        }
    }
}
