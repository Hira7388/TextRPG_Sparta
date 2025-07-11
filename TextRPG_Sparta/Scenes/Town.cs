using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Sparta.Data;
using TextRPG_Sparta.Managers;
using TextRPG_Sparta.UI;

namespace TextRPG_Sparta.Scenes
{
    public class Town : BaseScene
    {
        public override string SceneTitle => "마을";
        public override string SceneDescription => 
            "스파르타 마을에 오신 여러분 환영합니다.\n" +
            "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.";
        public Town(Player player,CoreUI coreUI) : base(player, coreUI) { }
        protected override void InitializeMenuOptions()
        {
            _menuOptions.Add("1", "상태보기");
            _menuOptions.Add("2", "인벤토리");
            _menuOptions.Add("3", "상점");
        }
        protected override void InitializeMenuActions()
        {
            _menuActions.Add("1", () => GameManager.Instance.ChangeScene(GameState.ViewStatus));
            _menuActions.Add("2", () => GameManager.Instance.ChangeScene(GameState.Inventory));
            _menuActions.Add("3", () => GameManager.Instance.ChangeScene(GameState.Store));
        }

        // BaseScene 생성자의 기본 UI출력
        public override void Show()
        {
            base.Show();
        }
    }
}
