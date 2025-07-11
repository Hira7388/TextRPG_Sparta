using System;
using TextRPG_Sparta.Data;
using TextRPG_Sparta.Managers;
using TextRPG_Sparta.UI;

namespace TextRPG_Sparta.Scenes
{
    public class ViewStatueScene : BaseScene
    {
        public ViewStatueScene(Player player, CoreUI coreUI) : base(player, coreUI) { }
        public override string SceneTitle => "상태 보기";
        public override string SceneDescription => "캐릭터의 정보가 표시됩니다.";
        protected override void InitializeMenuOptions()
        {
            _menuOptions.Add("0", "나가기");
        }
        protected override void InitializeMenuActions()
        {
            _menuActions.Add("0", () => GameManager.Instance.ChangeScene(GameState.Town));

        }

        public override void Show()
        {
            _coreUI.ShowHeader(SceneTitle, SceneDescription);
            ShowStatue();
            _coreUI.ShowMenu(_menuOptions);
            string input = _coreUI.GetUserInput();
            HandleInput(input);
        }

        // 스텟 보여주기
        private void ShowStatue()
        {
            Utils.SkipLine();
            Console.WriteLine($"Lv. {_player.playerStat.level}");
            Console.WriteLine($"{_player.name} ( {_player.chosenJob.name} )");
            Console.Write($"공격력 : {_player.playerStat.baseAtk}");
            if (_player.playerStat.additionalAtk > 0) Console.Write($" ( +{_player.playerStat.additionalAtk} )");
            Utils.SkipLine();
            Console.Write($"방어력 : {_player.playerStat.baseDef}");
            if (_player.playerStat.additionalDef > 0) Console.Write($" ( +{_player.playerStat.additionalDef} )");
            Utils.SkipLine();
            Console.Write($"체력 : {_player.playerStat.baseMaxHealth}");
            if (_player.playerStat.additionalMaxHealth > 0) Console.Write($" ( +{_player.playerStat.additionalMaxHealth} )");
            Utils.SkipLine();
        }
    }
}
