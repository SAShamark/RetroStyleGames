using Entities.Character.Abilities;
using Entities.Character.Data;
using Entities.Enemy;
using UnityEngine;
using Zenject;


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

        private CharacterController _characterController;
        private EnemySpawner _enemySpawner;

        [Inject]
        private void Construct(EnemySpawner enemySpawner, CharacterController characterController)
        {
            _enemySpawner = enemySpawner;
            _characterController = characterController;
        }

        private void Start()
        {
            CharacterStatsControl = new CharacterStatsControl(_characterData, this);
            _characterMovement = new CharacterMovement(_moveSpeed, transform);
            _characterCameraMovement = new CharacterCameraMovement(_characterCameraData, transform);
            UltimateSkill = new UltimateSkill(_enemySpawner, this);
            ShootingCharacter = new ShootingCharacter(_characterShootData, CharacterStatsControl);
        }

        private void FixedUpdate()
        {
            _characterMovement.MoveCharacter();
            _characterCameraMovement.CameraMovement();
        }
    }
}