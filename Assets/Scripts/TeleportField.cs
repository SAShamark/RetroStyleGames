using Entities;
using UnityEngine;

public class TeleportField : MonoBehaviour
{
    private const int CharacterLayer = 8;
    private ApplicationStart _applicationStart;

    private void Start()
    {
        _applicationStart=ApplicationStart.Instance;
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
        if (_applicationStart.EnemyRegistry.EnemiesContainer != null)
        {
            foreach (var enemy in _applicationStart.EnemyRegistry.EnemiesContainer)
            {
                enemy.ChangeTarget(other.transform.position);
            }
        }
    }
}