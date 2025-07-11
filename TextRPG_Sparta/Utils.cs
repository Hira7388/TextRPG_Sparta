using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Sparta
{
    public static class Utils
    {
        public static void SkipLine()
        {
            Console.WriteLine(" ");
        }

        public static void ShowInputPrompt()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }
    }
}
