using System.Linq;
using Entities;
using Entities.Enemy;
using Services;
using UnityEngine;

public class TeleportField : MonoBehaviour
{
    private const int CharacterLayer = 8;
    private EnemySpawner _enemySpawner;

    private void Start()
    {
        _enemySpawner = ServiceLocator.SharedInstance.Resolve<EnemySpawner>();
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
        var enemiesPools = _enemySpawner.EnemiesPools;
        if (enemiesPools != null)
        {
            foreach (var enemy in enemiesPools.SelectMany(enemiesPool => enemiesPool.Value.Pool))
            {
                enemy.ChangeTarget(other.transform.position);
            }
        }
    }
}