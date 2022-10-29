using UnityEngine;

namespace Entities.Character.Abilities
{
    public class UltaController : MonoBehaviour
    {

        private void Start()
        {
           // _characterController = CharacterController.Instanse;
            UIPanelController.OnUlta += KillAllEnemy;
        }

        private void OnDestroy()
        {
            UIPanelController.OnUlta -= KillAllEnemy;
        }


        private void KillAllEnemy()
        {
            /*if (_entitiesFactory.EnemiesContainer != null)
            {
                var enemyCount = _entitiesFactory.EnemiesContainer.Count;
                for (int i = enemyCount; i > 0; i--)
                {
                    var enemy = _entitiesFactory.EnemiesContainer.Last();
                    _entitiesFactory.EnemiesContainer.Remove(enemy);
                    Destroy(enemy.gameObject);
                }
            }*/

            //_characterController.DecreasePower(_characterController.Power);
        }
    }
}