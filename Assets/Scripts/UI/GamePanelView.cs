using UI.Panels.Death;
using UI.Panels.GamePlay;
using UI.Panels.Pause;
using UnityEngine;

namespace UI
{
    public class GamePanelView : MonoBehaviour
    {
        [SerializeField] private GamePlayView _gamePlayView;
        [SerializeField] private DeathView _deathView;
        [SerializeField] private PauseView _pauseView;
        public GamePlayView GamePlayView => _gamePlayView;
        public DeathView DeathView => _deathView;
        public PauseView PauseView => _pauseView;
    }
}