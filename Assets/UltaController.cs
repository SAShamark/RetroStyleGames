using Entities.Enemy;
using UnityEngine;

public class UltaController : MonoBehaviour
{
    private EnemySpawner _enemySpawner;

    private void Start()
    {
        _enemySpawner = EnemySpawner.Instance;
        UltaButton.OnUlta += KillAllEnemy;
    }

    private void OnDestroy()
    {
        UltaButton.OnUlta -= KillAllEnemy;
    }

    
    private void KillAllEnemy()
    {
        if (_enemySpawner.EnemiesContainer != null)
        {
            foreach (var enemy in _enemySpawner.EnemiesContainer)
            {
                Debug.LogError(enemy);
                _enemySpawner.EnemiesContainer.Remove(enemy);
                Destroy(enemy);
            } 
        }
        
    }
}