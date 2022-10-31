using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Panels.Pause
{
    public class PauseController : IViewController
    {
        public event Action<GameTab> OnPauseGame;

        private readonly PauseView _pauseView;
        private readonly PauseModel _pauseModel;

        public PauseController(PauseView pauseView, PauseModel pauseModel)
        {
            _pauseView = pauseView;
            _pauseModel = pauseModel;
        }

        public void Initialize(params object[] args)
        {
            _pauseView.RestartButton.onClick.AddListener(RestartButtonClicked);
            _pauseView.ContinueButton.onClick.AddListener(ContinueButtonClicked);
            TurnOnPanel();
            _pauseView.Show();
        }

        public void Dispose()
        {
            _pauseView.RestartButton.onClick.RemoveListener(RestartButtonClicked);
            _pauseView.ContinueButton.onClick.RemoveListener(ContinueButtonClicked);
            OnPauseGame?.Invoke(GameTab.GamePlay);
            _pauseView.Hide();
        }

        private void RestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        private void ContinueButtonClicked()
        {
            _pauseView.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void TurnOnPanel()
        {
            _pauseView.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}