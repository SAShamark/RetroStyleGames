using System;
using TMPro;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.Death
{
    public class DeathView : BaseView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _killCountText;
        public TMP_Text KillCountText => _killCountText;
        public Button RestartButton => _restartButton;
    }

    public class DeathController: IViewController
    {
        private readonly DeathView _deathView;
        private readonly DeathModel _deathModel;
        public event Action<DeathController> OnDeathViewRestartClicked;

        public DeathController(DeathView deathView,DeathModel deathModel)
        {
            _deathView = deathView;
            _deathModel = deathModel;
        }

        public void Initialize(params object[] args)
        {
            _deathView.RestartButton.onClick.AddListener(RestartButtonClicked);
            ChangeKillCount();
        }

        public void Hide()
        {
            _deathView.RestartButton.onClick.RemoveListener(RestartButtonClicked);
        }

        private void RestartButtonClicked()
        {
            OnDeathViewRestartClicked?.Invoke(this);
        }

        private void ChangeKillCount()
        {
            _deathView.KillCountText.text = _deathModel.KillCount.ToString();
        }
    }
}