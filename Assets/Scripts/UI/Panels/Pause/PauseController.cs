using System;
using UI.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Panels.Pause
{
    public class PauseController : IViewController
    {
        public event Action<GameTab> OnContinueGame;

        private readonly PauseView _pauseView;

        public PauseController(PauseView pauseView)
        {
            _pauseView = pauseView;
        }

        public void Initialize(params object[] args)
        {
            _pauseView.RestartButton.onClick.AddListener(RestartButtonClicked);
            _pauseView.ContinueButton.onClick.AddListener(ContinueButtonClicked);
            StoppingTime();
            _pauseView.Show();
        }

        public void Dispose()
        {
            _pauseView.RestartButton.onClick.RemoveListener(RestartButtonClicked);
            _pauseView.ContinueButton.onClick.RemoveListener(ContinueButtonClicked);
            _pauseView.Hide();
        }

        private void RestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        private void ContinueButtonClicked()
        {
            Time.timeScale = 1;
            OnContinueGame?.Invoke(GameTab.GamePlay);
        }

        private void StoppingTime()
        {
            Time.timeScale = 0;
        }
    }
}