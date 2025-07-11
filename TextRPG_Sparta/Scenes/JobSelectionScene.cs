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
    public class JobSelectionScene : BaseScene
    {
        public JobSelectionScene(Player player, CoreUI coreUI) : base(player, coreUI) { }

        public override string SceneTitle => "직업 선택";
        public override string SceneDescription => "원하는 직업을 선택해주세요";
        private List<Job> _jobs;

        protected override void InitializeMenuOptions()
        {
            _jobs = DataManager.Instance.AllJobs;
            for(int i = 0; i < _jobs.Count; i++)
            {
                _menuOptions.Add((i + 1).ToString(), $"{_jobs[i].name} : {_jobs[i].description}");
            }
        }

        protected override void InitializeMenuActions()
        {
            for(int i=0; i < _jobs.Count;i++)
            {
                int index = i;
                _menuActions.Add((i + 1).ToString(), () => SetJobAndStartGame(_jobs[index]));
            }
        }

        private void SetJobAndStartGame(Job job)
        {
            _player.SetJob(job);
            Console.WriteLine($"당신의 직업은 {job.name} 입니다.");
            Thread.Sleep(1000);
            // 이동
            GameManager.Instance.ChangeScene(GameState.Town);
        }

        // 가독성을 위해 출력
        public override void Show()
        {
            base.Show();
        }
    }
}
