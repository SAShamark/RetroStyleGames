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
            _gamePlayModel = new GamePlayModel(_characterController.CharacterStatsControl.Health,
                _characterController.CharacterStatsControl.Power);
            _deathModel = new DeathModel(_characterController.CharacterStatsControl.KillCount);
            _pauseModel = new PauseModel();

            _gamePlayController = new GamePlayController(_gamePanelView.GamePlayView, _gamePlayModel);
            _deathController = new DeathController(_gamePanelView.DeathView, _deathModel);
            _pauseController = new PauseController(_gamePanelView.PauseView, _pauseModel);


            _gamePlayController.OnPauseGame += OnTabChanger;
            _gamePlayController.OnShoot += _characterController.ShootingCharacter.GetProjectile;
            _gamePlayController.OnUltimateSkill += _characterController.UltimateSkill.UseSkill;

            _pauseController.OnPauseGame += OnTabChanger;

            _characterController.UltimateSkill.OnUltimateSkillButton += _gamePlayController.UltimateSkillInteractable;
            _characterController.CharacterStatsControl.OnDeath += OnTabChanger;
            _characterController.CharacterStatsControl.OnChangeHealth += _gamePlayController.UpdateHealthPoint;
            _characterController.CharacterStatsControl.OnChangePower += _gamePlayController.UpdatePowerPoint;


            _currentController = GetAndInitializeController(GameTab.GamePlay);
        }

        private void OnDestroy()
        {
            _gamePlayController.OnPauseGame -= OnTabChanger;
            _gamePlayController.OnShoot -= _characterController.ShootingCharacter.GetProjectile;
            _gamePlayController.OnUltimateSkill -= _characterController.UltimateSkill.UseSkill;

            _pauseController.OnPauseGame -= OnTabChanger;

            _characterController.UltimateSkill.OnUltimateSkillButton -= _gamePlayController.UltimateSkillInteractable;
            _characterController.CharacterStatsControl.OnDeath -= OnTabChanger;
            _characterController.CharacterStatsControl.OnChangeHealth -= _gamePlayController.UpdateHealthPoint;
            _characterController.CharacterStatsControl.OnChangePower -= _gamePlayController.UpdatePowerPoint;
        }

        private void OnTabChanger(GameTab gameTab)
        {
            _currentController?.Dispose();
            _currentController = GetAndInitializeController(gameTab);
        }

        private IViewController GetAndInitializeController(GameTab gameTab)
        {
            switch (gameTab)
            {
                case GameTab.GamePlay:
                    _gamePlayController.Initialize(_gamePlayModel);
                    return _gamePlayController;
                case GameTab.Death:
                    _deathController.Initialize(_deathModel);
                    return _deathController;
                case GameTab.Pause:
                    _pauseController.Initialize(_pauseModel);
                    return _pauseController;
                default:
                    return null;
            }
        }
    }
}