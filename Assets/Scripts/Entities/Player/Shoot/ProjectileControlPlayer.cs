using Entities.Enemy;
using UnityEngine;

namespace Entities.Player.Shoot
{
    public class ProjectileControlPlayer : ProjectileControl
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                
            }
        }

        protected override void Move()
        {
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * _moveSpeed));
            }
        }
    }
}