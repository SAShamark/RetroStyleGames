using Entities;
using Entities.Enemy;
using UnityEngine;
using Zenject;

public class TeleportField : MonoBehaviour
{
    private const int CharacterLayer = 8;
    private EnemyFactory _enemyFactory;

    [Inject]
    private void Construct(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
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
        if (_enemyFactory.EnemyRegistry.EnemiesContainer != null)
        {
            foreach (var enemy in _enemyFactory.EnemyRegistry.EnemiesContainer)
            {
                enemy.ChangeTarget(other.transform.position);
            }
        }
    }
}