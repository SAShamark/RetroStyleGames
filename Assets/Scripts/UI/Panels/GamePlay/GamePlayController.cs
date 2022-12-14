using System;
using System.Globalization;
using UI.View;
using UnityEngine;

namespace UI.Panels.GamePlay
{
    public class GamePlayController : IViewController
    {
        public event Action<GameTab> OnPauseGame;
        public event Action OnShoot;
        public event Action OnUltimateSkill;

        private readonly GamePlayView _gamePlayView;
        private readonly GamePlayModel _gamePlayModel;

        public GamePlayController(GamePlayView gamePlayView, GamePlayModel gamePlayModel)
        {
            _gamePlayView = gamePlayView;
            _gamePlayModel = gamePlayModel;
        }

        public void Initialize(params object[] args)
        {
            _gamePlayView.PauseButton.onClick.AddListener(PauseGameButtonClicked);
            _gamePlayView.ShootButton.onClick.AddListener(ShootButtonClicked);
            _gamePlayView.UltimateButton.onClick.AddListener(UltimateSkillButtonClicked);
            UpdateHealthPoint();
            UpdatePowerPoint();
            UpdateKillCount();
            _gamePlayView.Show();
        }

        public void Dispose()
        {
            _gamePlayView.PauseButton.onClick.RemoveListener(PauseGameButtonClicked);
            _gamePlayView.ShootButton.onClick.RemoveListener(ShootButtonClicked);
            _gamePlayView.UltimateButton.onClick.RemoveListener(UltimateSkillButtonClicked);
            _gamePlayView.Hide();
        }

        public void UpdateHealthPoint()
        {
            _gamePlayView.HealthText.text = $"{Mathf.Round(_gamePlayModel.Health).ToString(CultureInfo.InvariantCulture)} :Health";
        }

        public void UpdatePowerPoint()
        {
            _gamePlayView.PowerText.text = $"{Mathf.Round(_gamePlayModel.Power).ToString(CultureInfo.InvariantCulture)} :Power";
        }
        public void UpdateKillCount()
        {
            _gamePlayView.KillCount.text = $"{_gamePlayModel.KillCount.ToString()} :KillCount";
        }

        private void PauseGameButtonClicked()
        {
            OnPauseGame?.Invoke(GameTab.Pause);
        }

        private void ShootButtonClicked()
        {
            OnShoot?.Invoke();
        }

        private void UltimateSkillButtonClicked()
        {
            OnUltimateSkill?.Invoke();
        }

        public void UltimateSkillInteractable(bool isActive)
        {
            _gamePlayView.UltimateButton.interactable = isActive;
        }
    }
}