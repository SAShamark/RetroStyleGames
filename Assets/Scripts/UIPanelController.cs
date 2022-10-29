using System;
using Entities.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CharacterController = Entities.Character.CharacterController;

public class UIPanelController : MonoBehaviour
{
    public static event Action OnShoot;
    public static event Action OnUlta;

    [Header("Buttons")] [SerializeField] private Button _shootButton;
    [SerializeField] private Button _ultaButton;
    [SerializeField] private Button _loseRestartButton;
    [SerializeField] private Button _pauseRestartButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _continueButton;

    [Header("Panels")] [SerializeField] private GameObject _playPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _losePanel;

    [Header("Text")] [SerializeField] private TMP_Text _killCountText;
    private CharacterController _characterController;

    private void Start()
    {
        //_characterController = CharacterController.Instanse;
        //_characterController.OnDeath += LoseGame;
        //_characterController.OnUltaButton += Interactable;

        _shootButton.onClick.AddListener(Shoot);
        _ultaButton.onClick.AddListener(Ulta);
        _loseRestartButton.onClick.AddListener(RestartGame);
        _pauseRestartButton.onClick.AddListener(RestartGame);
        _pauseButton.onClick.AddListener(PauseGame);
        _continueButton.onClick.AddListener(ContinueGame);
    }

    private void OnDestroy()
    {
        _characterController.OnDeath -= LoseGame;
        _characterController.OnUltaButton -= Interactable;

        _shootButton.onClick.RemoveListener(Shoot);
        _ultaButton.onClick.RemoveListener(Ulta);
        _loseRestartButton.onClick.RemoveListener(RestartGame);
        _pauseRestartButton.onClick.RemoveListener(RestartGame);
        _pauseButton.onClick.RemoveListener(PauseGame);
        _continueButton.onClick.RemoveListener(ContinueGame);
    }

    private void Shoot()
    {
        OnShoot?.Invoke();
    }

    private void Ulta()
    {
        OnUlta?.Invoke();
    }

    private void Interactable(bool isActive)
    {
        _ultaButton.interactable = isActive;
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
        _killCountText.text = _characterController.KillCount.ToString();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}