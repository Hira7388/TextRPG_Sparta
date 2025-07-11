using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Sparta.Data;
using TextRPG_Sparta.UI;

namespace TextRPG_Sparta.Scenes
{
    public abstract class BaseScene
    {
        public abstract string SceneTitle { get; } // 자식 클래스가 씬 이름 구성하도록 강제
        public abstract string SceneDescription { get; } // 자식 클래스가 씬 설명 구성하도록 강제

        protected Player _player;
        protected CoreUI _coreUI;
        protected Dictionary<string, string> _menuOptions; // 메뉴 출력
        protected Dictionary<string, Action> _menuActions; // 메뉴 기능

        protected virtual void InitializeMenuOptions() { } // 자식 클래스가 매뉴 옵션을 구성하도록 강제 (기본적으로 비어있음)
        protected virtual void InitializeMenuActions() { } // 자식 클래스가 매뉴 액션을 구성하도록 강제
        public BaseScene(Player player, CoreUI coreUI)
        {
            this._player = player;
            this._coreUI = coreUI;
            _menuOptions = new Dictionary<string, string>();
            _menuActions = new Dictionary<string, Action>();

            InitializeMenuOptions();
            InitializeMenuActions();
        }

        // 게임 화면 출력
        public virtual void Show()
        {
            _coreUI.ShowHeader(SceneTitle, SceneDescription);
            _coreUI.ShowMenu(_menuOptions);
            string input = _coreUI.GetUserInput();
            HandleInput(input);
        }

        // 입력 액션 설정
        protected virtual void HandleInput(string input)
        {
            if (_menuActions.ContainsKey(input))
            {
                _menuActions[input].Invoke();
            }
            else
            {
                // 부모도 처리할 수 없는 잘못된 입력
                _coreUI.ShowWrongInput();
            }
        }
    }
}
