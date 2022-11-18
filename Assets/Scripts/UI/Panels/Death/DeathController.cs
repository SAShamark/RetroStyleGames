using UI.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Panels.Death
{
    public class DeathController: IViewController
    {
        private readonly DeathView _deathView;
        private int _killCount;

        public DeathController(DeathView deathView,int killCount)
        {
            _deathView = deathView;
            _killCount = killCount;
        }

        public void Initialize(params object[] args)
        {
            _deathView.RestartButton.onClick.AddListener(RestartButtonClicked);
            ChangeKillCount();
            StoppingTime();
            _deathView.Show();
        }

        public void Dispose()
        {
            _deathView.RestartButton.onClick.RemoveListener(RestartButtonClicked);
            _deathView.Hide();
        }
        public void GetKillCount(int killCount)
        {
            _killCount = killCount;
        }
        private void RestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        public void ChangeKillCount()
        {
            _deathView.KillCountText.text = $"{_killCount.ToString()} :KillCount";
        }
        private void StoppingTime()
        {
            Time.timeScale = 0;
        }
    }
}