using System;

namespace TextRPG_Sparta.UI
{
    // 모든 씬에서 공통적으로 사용하는 UI
    public class CoreUI
    {
        private static CoreUI _instance;
        public static CoreUI Instance
        {
            get
            {
                if (_instance == null) _instance = new CoreUI();
                return _instance;
            }
        }
        // 각 씬 입장시 현 씬의 이름, 씬 설명을 출력하는 메서드
        public void ShowHeader(string title, string description)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            // Scene 이름
            Console.WriteLine(title);
            Console.ResetColor();
            // Scene 설명
            Console.WriteLine(description);
        }

        // 메뉴 선택을 출력하는 메서드
        public void ShowMenu(Dictionary<string, string> menuOption)
        {
            Utils.SkipLine();
            foreach (var option in menuOption)
            {
                if (option.Key != "0")
                {
                    Console.WriteLine($"[{option.Key}] {option.Value}");
                }
            }
            if (menuOption.ContainsKey("0"))
                Console.WriteLine($"[{0}] {menuOption["0"]}");
            Utils.SkipLine();
        }

        public string GetUserInput()
        {
            Utils.ShowInputPrompt();
            return Console.ReadLine();
        }

        // 메뉴 선택 중 없는 번호를 누른 경우 경고 메세지를 출력하는 메서드
        public void ShowWrongInput()
        {
            Utils.SkipLine();
            Console.WriteLine("잘못된 입력입니다.");
            Utils.SkipLine();
            Thread.Sleep(1000);
        }
    }
}
