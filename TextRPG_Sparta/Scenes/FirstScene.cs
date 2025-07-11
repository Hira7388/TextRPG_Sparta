using System;
using TextRPG_Sparta.Data;
using TextRPG_Sparta.Managers;
using TextRPG_Sparta.UI;

namespace TextRPG_Sparta.Scenes
{
    public class FirstScene : BaseScene
    {
        public FirstScene(Player player, CoreUI coreUI) : base(player, coreUI) { }
        public override string SceneTitle => "캐릭터 생성";
        public override string SceneDescription => 
            "스파르타 던전에 오신 여러분을 환영합니다.\n" + 
            "원하시는 이름을 입력해주세요";

        private string playerName;
        protected override void InitializeMenuOptions()
        {
            _menuOptions.Add("1", "저장");
            _menuOptions.Add("2", "취소");
        }

        protected override void InitializeMenuActions()
        {
            _menuActions.Add("1", Save);
            _menuActions.Add("2", Cancle);
        }

        public override void Show()
        {
            _coreUI.ShowHeader(SceneTitle, SceneDescription);
            Console.Write("\n>> ");
            playerName = Console.ReadLine();
            Utils.SkipLine();
            Console.WriteLine("입력하신 이름은 {0} 입니다", playerName);

            _coreUI.ShowMenu(_menuOptions);

            string input = _coreUI.GetUserInput();
            HandleInput(input);
        }

        public void Save()
        {
            _player.SetName(playerName);
            Console.WriteLine("캐릭터 생성이 완료되었습니다!");
            Thread.Sleep(1000);
            GameManager.Instance.ChangeScene(GameState.JobSelect);
        }

        public void Cancle()
        {
            return;
        }
    }
}
