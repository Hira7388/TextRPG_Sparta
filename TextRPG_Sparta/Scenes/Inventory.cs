using System;
using TextRPG_Sparta.Data;
using TextRPG_Sparta.Managers;
using TextRPG_Sparta.UI;

namespace TextRPG_Sparta.Scenes
{
    public class Inventory : BaseScene
    {
        public Inventory(Player player, CoreUI coreUI) : base(player, coreUI) { }

        public override string SceneTitle => _isEquipMode ? "인벤토리 - 장착 관리" : "인벤토리";
        public override string SceneDescription => _isEquipMode ? "보유 중인 아이템을 장착 / 해제할 수 있습니다." : "보유 중인 아이템을 관리할 수 있습니다.";
        private bool _isEquipMode = false; // 장착 모드인가 아닌가
        protected override void InitializeMenuOptions()
        {
            _menuOptions.Add("1", "장착관리");
            _menuOptions.Add("0", "나가기");
        }

        protected override void InitializeMenuActions()
        {
            _menuActions.Add("1", () => _isEquipMode = true);
            _menuActions.Add("0", () => GameManager.Instance.ChangeScene(GameState.Town));
        }

        public override void Show()
        {
            while (true)
            {
                _coreUI.ShowHeader(SceneTitle, SceneDescription);
                DisplayInventoryItems();

                if (_isEquipMode) // 장착 관리 모드일 때
                {
                    Utils.SkipLine();
                    Console.WriteLine("\n[0] 나가기");
                    Utils.SkipLine();
                    string input = _coreUI.GetUserInput();
                    if (input == "0")
                    {
                        _isEquipMode = false; // 모드 해제
                        continue; // 루프의 처음으로 돌아감
                    }
                    HandleEquipInput(input);
                }
                else
                {
                    Utils.SkipLine();
                    _coreUI.ShowMenu(_menuOptions);
                    string input = _coreUI.GetUserInput();
                    HandleInput(input);
                    if (input == "0") break; // 0번 입력 시 while 루프 탈출
                }
            }
        }

        // 인벤토리 내 아이템을 보여주는 메서드
        private void DisplayInventoryItems()
        {
            Utils.SkipLine();
            Console.WriteLine("[아이템 목록]");
            if (_player.inventory.Count == 0)
            {
                Utils.SkipLine();
                Console.Write("보유 중인 아이템이 없습니다.");
                return;
            }

            for(int i = 0; i < _player.inventory.Count; i++)
            {
                Console.Write("- ");

                if (_isEquipMode) // 장착 관리 모드에 들어가면 출력
                {
                    Console.Write($"{i + 1}. ");
                }
                if (_player.inventory[i] == _player.equippedWeapon || _player.inventory[i] == _player.equippedArmor) // 장착중인 아이템이라면 출력
                {
                    Console.Write("[E] ");
                }
                Console.Write($" {_player.inventory[i].Name} | {_player.inventory[i].Description}");
                // 아이템 능력치 표시
                if (_player.inventory[i].BonusAtk > 0) Console.Write($" | 공격력 +{_player.inventory[i].BonusAtk}");
                if (_player.inventory[i].BonusDef > 0) Console.Write($" | 방어력 +{_player.inventory[i].BonusDef}");
                if (_player.inventory[i].BonusHp > 0) Console.Write($" | 체력 +{_player.inventory[i].BonusHp}");
                Utils.SkipLine();
            }
        }

        // 장착 관리 모드에서 추가 입력을 처리하는 메서드
        private void HandleEquipInput(string input)
        {
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= _player.inventory.Count)
            {
                Item selectedItem = _player.inventory[choice - 1];
                _player.EquipOrUnequipItem(selectedItem);
            }
            else
            {
                _coreUI.ShowWrongInput();
            }
        }
    }
}
