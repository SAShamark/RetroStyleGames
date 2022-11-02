using Entities.Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startCharacterPosition;
        [SerializeField] private Entities.Character.CharacterController _characterPrefab;
        [FormerlySerializedAs("_enemyFactory")] [SerializeField] private EnemySpawner _enemySpawner;

        public override void InstallBindings()
        {
            BindCharacter();
            BindEnemyFactory();
        }

        private void BindCharacter()
        {
            Entities.Character.CharacterController characterController =
                Container.InstantiatePrefabForComponent<Entities.Character.CharacterController>(_characterPrefab,
                    _startCharacterPosition.position, Quaternion.identity, null);
            Container.Bind<Entities.Character.CharacterController>().FromInstance(characterController).AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.Bind<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
        }
    }
}