using TMPro;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.GamePlay
{
    public class GamePlayView : BaseView
    {
        [SerializeField] private Button _shootButton;
        [SerializeField] private Button _ultimateButton;
        [SerializeField] private Button _pauseButton;

        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _powerText;
        [SerializeField] private TMP_Text _killCount;

        public Button ShootButton => _shootButton;
        public Button UltimateButton => _ultimateButton;
        public Button PauseButton => _pauseButton;

        public TMP_Text HealthText => _healthText;
        public TMP_Text PowerText => _powerText;
        public TMP_Text KillCount => _killCount;
    }
}