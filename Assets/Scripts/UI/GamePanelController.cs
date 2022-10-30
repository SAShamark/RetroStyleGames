using UI.Panels;
using UI.Panels.Death;
using UI.Panels.GamePlay;
using UI.Panels.Pause;
using UnityEngine;
using Zenject;
using CharacterController = Entities.Character.CharacterController;

namespace UI
{
    public class GamePanelController : MonoBehaviour
    {
        [SerializeField] private GamePanelView _gamePanelView;

        private GamePlayController _gamePlayController;
        private GamePlayModel _gamePlayModel;

        private DeathController _deathController;
        private DeathModel _deathModel;

        private PauseController _pauseController;
        private PauseModel _pauseModel;

        private IViewController _currentController;

        private CharacterController _characterController;

        [Inject]
        private void Construct(CharacterController characterController)
        {
            _characterController = characterController;
        }

        private void Start()
        {
            _gamePlayModel = new GamePlayModel();
            _deathModel = new DeathModel(_characterController.CharacterStatsControl.KillCount);
            _pauseModel = new PauseModel();

            _gamePlayController = new GamePlayController();
            _deathController = new DeathController(_gamePanelView.DeathView, _deathModel);
            _pauseController = new PauseController();


            _currentController = GetAndInitializeController(CreationTab.GamePlay);
        }

        private void OnTabChanger(CreationTab creationTab)
        {
            _currentController?.Hide();
            _currentController = GetAndInitializeController(creationTab);
        }

        private IViewController GetAndInitializeController(CreationTab creationTab)
        {
            switch (creationTab)
            {
                case CreationTab.GamePlay:
                    _gamePlayController.Initialize(_gamePlayModel);
                    return _gamePlayController;
                case CreationTab.Death:
                    _deathController.Initialize(_deathModel);
                    return _deathController;
                case CreationTab.Pause:
                    _pauseController.Initialize(_pauseModel);
                    return _pauseController;
                default:
                    return null;
            }
        }
    }

    internal interface IViewController
    {
        void Initialize(params object[] args);
        void Hide();
    }
}