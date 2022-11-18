using System.Linq;
using Entities.Enemy;
using Services;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TeleportField _teleportField;
    private EnemySpawner _enemySpawner;

    private void Start()
    {
        _enemySpawner = ServiceLocator.SharedInstance.Resolve<EnemySpawner>();

        _teleportField.OnCharacterTeleported += InformingEnemyAboutCharacterPosition;
    }

    private void OnDestroy()
    {
        _teleportField.OnCharacterTeleported -= InformingEnemyAboutCharacterPosition;
    }

    private void InformingEnemyAboutCharacterPosition(Vector3 characterPosition)
    {
        var enemiesPools = _enemySpawner.EnemiesPools;
        if (enemiesPools != null)
        {
            foreach (var enemy in enemiesPools.SelectMany(enemiesPool => enemiesPool.Value.Pool))
            {
                enemy.ChangeTarget(characterPosition);
            }
        }
    }
}