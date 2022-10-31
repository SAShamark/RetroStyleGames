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
}