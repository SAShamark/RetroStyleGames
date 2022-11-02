using Entities;
using Entities.Enemy;
using UnityEngine;
using Zenject;

public class TeleportField : MonoBehaviour
{
    private const int CharacterLayer = 8;
    private EnemySpawner _enemySpawner;

    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == CharacterLayer)
        {
            other.transform.position = PositionProcessor.GetNewPosition();
            InformingEnemyAboutCharacterPosition(other);
        }
    }

    private void InformingEnemyAboutCharacterPosition(Collider other)
    {
        if (_enemySpawner.EnemyRegistry.EnemiesContainer != null)
        {
            foreach (var enemy in _enemySpawner.EnemyRegistry.EnemiesContainer)
            {
                enemy.ChangeTarget(other.transform.position);
            }
        }
    }
}