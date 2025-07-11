using System;
using TextRPG_Sparta.Data;
using TextRPG_Sparta.Scenes;
using TextRPG_Sparta.UI;

namespace TextRPG_Sparta.Managers
{
    public class GameManager
    {
        // 플레이어 데이터
        private Player _player;
        public Player Player => _player;

        // 플레이어가 현재 있는 위치 데이터
        private BaseScene _currentScene;
        private Dictionary<GameState, BaseScene> _scenes;

        // 씬 정보
        private CoreUI _coreUI;

        // 게임매니저 싱글톤
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null) _instance = new GameManager();
                return _instance;
            }
        }

        // 게임 매니저 생성자
        private GameManager()
        {
            _player = new Player(""); // 첫 생성시 이름이 없고 FirstScene에서 이름을 넣는다.
            _coreUI = CoreUI.Instance;
            _scenes = new Dictionary<GameState, BaseScene>
            {
                { GameState.First, new FirstScene(_player, _coreUI) },
                { GameState.JobSelect, new JobSelectionScene(_player, _coreUI) },
                { GameState.Town, new Town(_player, _coreUI) },
                { GameState.Store, new Store(_player, _coreUI) },
                { GameState.Inventory, new Inventory(_player, _coreUI) },
                { GameState.ViewStatus, new ViewStatueScene(_player, _coreUI) },
                // 씬이 추가되면 여기에 추가
            };
        }

        public void ChangeScene(GameState state)
        {
            // 딕셔너리에서 GameState 키에 해당하는 씬 객체를 찾아 현재 씬으로 교체
            if (_scenes.ContainsKey(state))
            {
                _currentScene = _scenes[state];
            }
        }

        public void GameLoop()
        {
            _currentScene = _scenes[GameState.First];
            while (true)
            {
                _currentScene.Show();
            }
        }
    }
}
