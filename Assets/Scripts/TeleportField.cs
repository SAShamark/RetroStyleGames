using Entities;
using UnityEngine;

public class TeleportField : MonoBehaviour
{
    private const int CharacterLayer = 8;
    private ServiceContainer _serviceContainer;

    private void Start()
    {
        _serviceContainer = ServiceContainer.Instance;
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
        if (_serviceContainer.EnemyRegistry.EnemiesContainer != null)
        {
            foreach (var enemy in _serviceContainer.EnemyRegistry.EnemiesContainer)
            {
                enemy.ChangeTarget(other.transform.position);
            }
        }
    }
}