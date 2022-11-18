using Services;
using UI.Panels.Death;
using UI.Panels.GamePlay;
using UI.Panels.Pause;
using UI.View;
using CharacterController = Entities.Character.CharacterController;

namespace UI
{
    public class GamePanelsController
    {
        private readonly GamePanelView _gamePanelView;

        private GamePlayController _gamePlayController;
        private GamePlayModel _gamePlayModel;

        private DeathController _deathController;
        private PauseController _pauseController;

        private IViewController _currentController;

        private CharacterController _characterController;


        public GamePanelsController(GamePanelView gamePanelView)
        {
            _gamePanelView = gamePanelView;
        }

        public void Init()
        {
            _characterController = ServiceLocator.SharedInstance.Resolve<CharacterController>();
            
            _gamePlayModel = new GamePlayModel(_characterController.CharacterStatsControl.Health,
                _characterController.CharacterStatsControl.Power, _characterController.CharacterStatsControl.KillCount);

            _gamePlayController = new GamePlayController(_gamePanelView.GamePlayView, _gamePlayModel);
            _deathController = new DeathController(_gamePanelView.DeathView, _characterController.CharacterStatsControl.KillCount);
            _pauseController = new PauseController(_gamePanelView.PauseView);


            _gamePlayController.OnPauseGame += OnTabChanger;
            _gamePlayController.OnShoot += _characterController.CharacterShooting.Shoot;
            _gamePlayController.OnUltimateSkill += _characterController.UltimateSkill.UseSkill;

            _pauseController.OnContinueGame += OnTabChanger;

            _characterController.UltimateSkill.OnUltimateSkillButton += _gamePlayController.UltimateSkillInteractable;
            _characterController.CharacterStatsControl.OnChangeTab += OnTabChanger;
            _characterController.CharacterStatsControl.OnChangeHealth += UpdateGamePlayModelHealth;
            _characterController.CharacterStatsControl.OnChangeHealth += _gamePlayController.UpdateHealthPoint;
            _characterController.CharacterStatsControl.OnChangePower += UpdateGamePlayModelPower;
            _characterController.CharacterStatsControl.OnChangePower += _gamePlayController.UpdatePowerPoint;
            _characterController.CharacterStatsControl.OnChangeKillCount += UpdateGamePlayModelKillCount;
            _characterController.CharacterStatsControl.OnChangeKillCount += _gamePlayController.UpdateKillCount;
            _characterController.CharacterStatsControl.OnChangeKillCount += GetDeathControllerKillCount;
            _characterController.CharacterStatsControl.OnChangeKillCount += _deathController.ChangeKillCount;

            _currentController = GetAndInitializeController(GameTab.GamePlay);
        }

        public void OnDestroy()
        {
            _gamePlayController.OnPauseGame -= OnTabChanger;
            _gamePlayController.OnShoot -= _characterController.CharacterShooting.Shoot;
            _gamePlayController.OnUltimateSkill -= _characterController.UltimateSkill.UseSkill;

            _pauseController.OnContinueGame -= OnTabChanger;

            _characterController.UltimateSkill.OnUltimateSkillButton -= _gamePlayController.UltimateSkillInteractable;
            _characterController.CharacterStatsControl.OnChangeTab -= OnTabChanger;
            _characterController.CharacterStatsControl.OnChangeHealth -= UpdateGamePlayModelHealth;
            _characterController.CharacterStatsControl.OnChangeHealth -= _gamePlayController.UpdateHealthPoint;
            _characterController.CharacterStatsControl.OnChangePower -= UpdateGamePlayModelPower;
            _characterController.CharacterStatsControl.OnChangePower -= _gamePlayController.UpdatePowerPoint;
            _characterController.CharacterStatsControl.OnChangeKillCount -= UpdateGamePlayModelKillCount;
            _characterController.CharacterStatsControl.OnChangeKillCount -= _gamePlayController.UpdateKillCount;
            _characterController.CharacterStatsControl.OnChangeKillCount -= GetDeathControllerKillCount;
            _characterController.CharacterStatsControl.OnChangeKillCount -= _deathController.ChangeKillCount;
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
                    _deathController.Initialize();
                    return _deathController;
                case GameTab.Pause:
                    _pauseController.Initialize();
                    return _pauseController;
                default:
                    return null;
            }
        }

        private void UpdateGamePlayModelHealth()
        {
            _gamePlayModel.UpdateHealth(_characterController.CharacterStatsControl.Health);
        }

        private void UpdateGamePlayModelPower()
        {
            _gamePlayModel.UpdatePower(_characterController.CharacterStatsControl.Power);
        }

        private void UpdateGamePlayModelKillCount()
        {
            _gamePlayModel.UpdateKillCount(_characterController.CharacterStatsControl.KillCount);
        }

        private void GetDeathControllerKillCount()
        {
            _deathController.GetKillCount(_characterController.CharacterStatsControl.KillCount);
        }
    }
}