using System;
using TextRPG_Sparta.Data;
using TextRPG_Sparta.Managers;
using TextRPG_Sparta.UI;

namespace TextRPG_Sparta.Scenes
{
    public class Store : BaseScene
    {
        // 상점 모드
        enum StoreMode
        {
            main,
            purchase,
            sell
        }

        public Store(Player player, CoreUI coreUI) : base(player, coreUI) { }
        public override string SceneTitle => "상점";
        public override string SceneDescription => "필요한 아이템을 얻을 수 있는 상점입니다.";
        //private bool _isPurchaseMode = false; // 구매 모드인가 아닌가 -> 판매 모드까지 생겨서 enum으로 관리

        // 상점 모드 관리
        private StoreMode _currentMode = StoreMode.main;

        public override void Show()
        {
            while (true) // 플레이어가 0번으로 나가기 전까지 상점 씬에 머무름
            {
                _coreUI.ShowHeader(SceneTitle, SceneDescription);
                Utils.SkipLine();
                ShowPlayerMoney();

                switch(_currentMode)
                {
                    case StoreMode.main:
                        DisplayStoreItems(false);
                        if (!HandleMainMenuInput()) return;
                        break;
                    case StoreMode.purchase:
                        DisplayStoreItems(true);
                        HandlePurchaseInput();
                        break;
                    case StoreMode.sell:
                        DisplayPlayerInventory();
                        HandleSellInput();
                        break;
                }
            }
        }

        // InitializeMenuOption과 Action을 대신하는 메서드
        private bool HandleMainMenuInput()
        { 
            var mainMenu = new Dictionary<string, string>()
            {
                { "1", "아이템 구매" },
                { "2", "아이템 판매" },
                { "0", "나가기" }
            };
            _coreUI.ShowMenu(mainMenu);
            string input = _coreUI.GetUserInput();

            switch (input)
            {
                case "1": _currentMode = StoreMode.purchase; return true;
                case "2": _currentMode = StoreMode.sell; return true;
                case "0": GameManager.Instance.ChangeScene(GameState.Town); return false;
                default: _coreUI.ShowWrongInput(); return true;
            }
        }

        // 구매 모드 시 추가 입력 처리
        private void HandlePurchaseInput()
        {
            Utils.SkipLine();
            Console.WriteLine("\n0. 나가기");
            string input = _coreUI.GetUserInput();
            if (int.TryParse(input, out int choice))
            {
                if (choice == 0) { _currentMode = StoreMode.main; return; }
                Item[] storeItems = DataManager.Instance.AllItems;
                int itemIndex = choice - 1;
                if (itemIndex >= 0 && itemIndex < storeItems.Length) AttemptToBuyItem(storeItems[itemIndex]);
                else _coreUI.ShowWrongInput();
            }
            else _coreUI.ShowWrongInput();
        }

        // 판매 모드 시 추가 입력 처리
        private void HandleSellInput()
        {
            Utils.SkipLine();
            Console.WriteLine("\n0. 나가기");
            string input = _coreUI.GetUserInput();
            if (int.TryParse(input, out int choice))
            {
                if (choice == 0) { _currentMode = StoreMode.main; return; }
                int itemIndex = choice - 1;
                if (itemIndex >= 0 && itemIndex < _player.inventory.Count)
                {
                    AttemptToSellItem(_player.inventory[itemIndex]);
                }
                else _coreUI.ShowWrongInput();
            }
            else _coreUI.ShowWrongInput();
        }

        // 입력한 번호의 아이템을 구매 시도하는 메서드
        private void AttemptToBuyItem(Item item)
        {
            if (_player.inventory.Contains(item)) Console.WriteLine("이미 구매한 아이템입니다.");
            else if (_player.gold < item.Price) Console.WriteLine("골드가 부족합니다.");
            else
            {
                _player.gold -= item.Price;
                _player.inventory.Add(item);
                Console.WriteLine($"'{item.Name}' 을(를) 구매했습니다.");
            }
            Thread.Sleep(1000);
        }

        // 입력한 번호의 아이템을 판매 시도하는 메서드
        private void AttemptToSellItem(Item item)
        {
            int sellPrice = (int)(item.Price * 0.85); // 판매가는 구매가의 85%

            _player.SellItem(item); // Player의 판매 로직 호출 (장비 해제 및 인벤토리 제거)
            _player.gold += sellPrice;

            Console.WriteLine($"'{item.Name}' 을(를) {sellPrice} G 에 판매했습니다.");
            Thread.Sleep(1000);
        }

        // 플레이어 인벤토리 목록 표시 (구매용)
        private void DisplayStoreItems(bool showNumbers)
        {
            Utils.SkipLine();
            Console.WriteLine("\n[아이템 목록]");
            Item[] storeItems = DataManager.Instance.AllItems;
            for (int i = 0; i < storeItems.Length; i++)
            {
                Item item = storeItems[i];
                Console.Write("- ");
                if (showNumbers) Console.Write($"{i + 1}. ");
                Console.Write($"{item.Name,-10} |");
                if (item.BonusAtk > 0) Console.Write($" 공격력 +{item.BonusAtk,-3} |");
                if (item.BonusDef > 0) Console.Write($" 방어력 +{item.BonusDef,-3} |");
                if (item.BonusHp > 0) Console.Write($" 체  력 +{item.BonusHp,-3} |");
                Console.Write($" {item.Description,-30} |");
                if (_player.inventory.Contains(item)) Console.Write(" [구매 완료]");
                else Console.Write($" {item.Price} G");
                Console.WriteLine();
            }
        }

        // 플레이어 인벤토리 목록 표시 (판매용)
        private void DisplayPlayerInventory()
        {
            Console.WriteLine("\n[판매할 아이템 목록]");
            if (_player.inventory.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                return;
            }
            for (int i = 0; i < _player.inventory.Count; i++)
            {
                Item item = _player.inventory[i];
                int sellPrice = (int)(item.Price * 0.85);
                Console.Write($"- {i + 1}. {item.Name,-10} | 판매가: {sellPrice} G");
                Console.WriteLine();
            }
        }
        // 보유 골드 보여주기
        public void ShowPlayerMoney()
        {
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"Gold : {_player.gold}");
        }

        private string GetTitleByMode()
        {
            switch (_currentMode)
            {
                case StoreMode.purchase: return "상점 - 아이템 구매";
                case StoreMode.sell: return "상점 - 아이템 판매";
                default: return "상점";
            }
        }
    }
}

