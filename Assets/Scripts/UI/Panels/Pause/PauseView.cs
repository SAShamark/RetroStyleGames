using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.Pause
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        public Button RestartButton => _restartButton;
        public Button ContinueButton => _continueButton;
    }
}