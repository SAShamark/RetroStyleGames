using System;
using UI.Panels;
using UI.Panels.Death;
using UI.Panels.GamePlay;
using UI.Panels.Pause;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GamePanelView : MonoBehaviour
    {
        public static event Action OnShoot;
        public static event Action OnUltimateSkill;

        [SerializeField] private GamePlayView _gamePlayView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private PauseView _pauseView;
        public GamePlayView GamePlayView => _gamePlayView;
        public DeathView DeathView => _deathView;
        public PauseView PauseView => _pauseView;

        private Entities.Character.CharacterController _characterController;

        


/*private void Start()
{
    _characterController.CharacterStatsControl.OnDeath += LoseGame;
    _characterController.CharacterStatsControl.OnUltaButton += Interactable;

    _shootButton.onClick.AddListener(Shoot);
    _ultimateButton.onClick.AddListener(UltimateSkill);
    _loseRestartButton.onClick.AddListener(RestartGame);
    _pauseRestartButton.onClick.AddListener(RestartGame);
    _pauseButton.onClick.AddListener(PauseGame);
    _continueButton.onClick.AddListener(ContinueGame);
}

private void OnDestroy()
{
    _characterController.CharacterStatsControl.OnDeath -= LoseGame;
    _characterController.CharacterStatsControl.OnUltaButton -= Interactable;

    _shootButton.onClick.RemoveListener(Shoot);
    _ultimateButton.onClick.RemoveListener(UltimateSkill);
    _loseRestartButton.onClick.RemoveListener(RestartGame);
    _pauseRestartButton.onClick.RemoveListener(RestartGame);
    _pauseButton.onClick.RemoveListener(PauseGame);
    _continueButton.onClick.RemoveListener(ContinueGame);
}

private void Shoot()
{
    OnShoot?.Invoke();
}

private void UltimateSkill()
{
    OnUltimateSkill?.Invoke();
}

private void Interactable(bool isActive)
{
    _ultimateButton.interactable = isActive;
}

private void PauseGame()
{
    Time.timeScale = 0;
    _pausePanel.SetActive(true);
}

private void ContinueGame()
{
    Time.timeScale = 1;
    _pausePanel.SetActive(false);
}

private void LoseGame()
{
    _playPanel.SetActive(false);
    _losePanel.SetActive(true);
    Time.timeScale = 0;
    _killCountText.text = _characterController.CharacterStatsControl.KillCount.ToString();
}

private void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    Time.timeScale = 1;
}*/
    }
}