using UI.Panels.Death;
using UI.Panels.GamePlay;
using UI.Panels.Pause;
using CharacterController = Entities.Character.CharacterController;

namespace UI
{
    public class GamePanelsController
    {
        private readonly GamePanelView _gamePanelView;

        private GamePlayController _gamePlayController;
        private GamePlayModel _gamePlayModel;

        private DeathController _deathController;
        private DeathModel _deathModel;

        private PauseController _pauseController;
        private PauseModel _pauseModel;

        private IViewController _currentController;

        private readonly CharacterController _characterController;


        public GamePanelsController(GamePanelView gamePanelView, CharacterController characterController)
        {
            _gamePanelView = gamePanelView;
            _characterController = characterController;
        }

        public void Init()
        {
            _gamePlayModel = new GamePlayModel(_characterController.CharacterStatsControl.Health,
                _characterController.CharacterStatsControl.Power, _characterController.CharacterStatsControl.KillCount);
            _deathModel = new DeathModel(_characterController.CharacterStatsControl.KillCount);
            _pauseModel = new PauseModel();

            _gamePlayController = new GamePlayController(_gamePanelView.GamePlayView, _gamePlayModel);
            _deathController = new DeathController(_gamePanelView.DeathView, _deathModel);
            _pauseController = new PauseController(_gamePanelView.PauseView, _pauseModel);


            _gamePlayController.OnPauseGame += OnTabChanger;
            _gamePlayController.OnShoot += _characterController.ShootingCharacter.GetProjectile;
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
            _characterController.CharacterStatsControl.OnChangeKillCount += GetDeathModelKillCount;
            _characterController.CharacterStatsControl.OnChangeKillCount += _deathController.ChangeKillCount;

            _currentController = GetAndInitializeController(GameTab.GamePlay);
        }

        public void OnDestroy()
        {
            _gamePlayController.OnPauseGame -= OnTabChanger;
            _gamePlayController.OnShoot -= _characterController.ShootingCharacter.GetProjectile;
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
            _characterController.CharacterStatsControl.OnChangeKillCount -= GetDeathModelKillCount;
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
                    _deathController.Initialize(_deathModel);
                    return _deathController;
                case GameTab.Pause:
                    _pauseController.Initialize(_pauseModel);
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

        private void GetDeathModelKillCount()
        {
            _deathModel.GetKillCount(_characterController.CharacterStatsControl.KillCount);
        }
    }
}