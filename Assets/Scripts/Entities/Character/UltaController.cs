using System.Linq;
using Entities.Enemy;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Player
{
    public class UltaController : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        private CharacterController _characterController;

        private void Start()
        {
            _enemySpawner = EnemySpawner.Instance;
            _characterController = CharacterController.Instanse;
            UIPanelController.OnUlta += KillAllEnemy;
        }

        private void OnDestroy()
        {
            UIPanelController.OnUlta -= KillAllEnemy;
        }


        private void KillAllEnemy()
        {
            if (_enemySpawner.EnemiesContainer != null)
            {
                var enemyCount = _enemySpawner.EnemiesContainer.Count;
                for (int i = enemyCount; i > 0; i--)
                {
                    var enemy = _enemySpawner.EnemiesContainer.Last();
                    _enemySpawner.EnemiesContainer.Remove(enemy);
                    Destroy(enemy.gameObject);
                }
            }

            _characterController.DecreasePower(_characterController.Power);
        }
    }
}