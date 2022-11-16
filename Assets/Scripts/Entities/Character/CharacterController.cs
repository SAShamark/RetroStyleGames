using Entities.Character.Controllers;
using Entities.Character.Data;
using Entities.Enemy;
using Services;
using UnityEngine;


namespace Entities.Character
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterStatsControl CharacterStatsControl { get; private set; }
        public UltimateSkill UltimateSkill { get; private set; }
        public ShootingCharacter ShootingCharacter { get; private set; }

        [SerializeField] [Range(2f, 10f)] private float _moveSpeed = 8;
        [SerializeField] private CharacterCameraData _characterCameraData;
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private CharacterShootData _characterShootData;

        private CharacterMovement _characterMovement;
        private CharacterCameraMovement _characterCameraMovement;
        private EnemySpawner _enemySpawner;

        private void Awake()
        {
            ServiceLocator.SharedInstance.Register(this);
        }

        private void Start()
        {
            _enemySpawner = ServiceLocator.SharedInstance.Resolve<EnemySpawner>();

            CharacterStatsControl = new CharacterStatsControl(_characterData);
            _characterMovement = new CharacterMovement(_moveSpeed, transform);
            _characterCameraMovement = new CharacterCameraMovement(_characterCameraData, transform);
            UltimateSkill = new UltimateSkill(_enemySpawner, CharacterStatsControl);
            ShootingCharacter = new ShootingCharacter(_enemySpawner, _characterShootData, CharacterStatsControl);

            CharacterStatsControl.OnMaxPower += UltimateSkill.UltimatePerformance;
        }

        private void FixedUpdate()
        {
            _characterMovement.MoveCharacter();
            _characterCameraMovement.CameraMovement();
        }

        private void OnDestroy()
        {
            CharacterStatsControl.OnMaxPower -= UltimateSkill.UltimatePerformance;
        }
    }
}