using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Panels.Death
{
    public class DeathController: IViewController
    {
        private readonly DeathView _deathView;
        private readonly DeathModel _deathModel;

        public DeathController(DeathView deathView,DeathModel deathModel)
        {
            _deathView = deathView;
            _deathModel = deathModel;
        }

        public void Initialize(params object[] args)
        {
            _deathView.RestartButton.onClick.AddListener(RestartButtonClicked);
            ChangeKillCount();
            _deathView.Show();
        }

        public void Dispose()
        {
            _deathView.RestartButton.onClick.RemoveListener(RestartButtonClicked);
            _deathView.Hide();
        }

        private void RestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        private void ChangeKillCount()
        {
            _deathView.KillCountText.text = _deathModel.KillCount.ToString();
        }
    }
}