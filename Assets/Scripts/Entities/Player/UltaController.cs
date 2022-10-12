using System.Linq;
using Entities.Enemy;
using UnityEngine;

public class UltaController : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private PlayerController _playerController;

    private void Start()
    {
        _enemySpawner = EnemySpawner.Instance;
        _playerController = PlayerController.Instanse;
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
                Debug.LogError(enemy);
                _enemySpawner.EnemiesContainer.Remove(enemy);

                Destroy(enemy.gameObject);
            }
        }

        _playerController.DecreasePower(_playerController.Power);
    }
}