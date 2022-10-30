using System.Collections;
using UnityEngine;

namespace Entities
{
    public abstract class Projectile : MonoBehaviour
    {
        public float AttackValue { get; set; }
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] protected float _moveSpeed = 3f;
        [SerializeField] protected float _lifeTime = 5;
        protected IEnumerator TurnOffProjectileCoroutine;


        private void Update()
        {
            MoveProjectile();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var triggerEnterCountLayer = ((1 << other.gameObject.layer) & _layerMask);

            if (triggerEnterCountLayer != 0)
            {
                if (TurnOffProjectileCoroutine != null)
                {
                    StopCoroutine(TurnOffProjectileCoroutine);
                }
            }
        }


        protected virtual void OnDisable()
        {
            if (TurnOffProjectileCoroutine != null)
            {
                StopCoroutine(TurnOffProjectileCoroutine);
            }
        }

        protected abstract IEnumerator TurnOffProjectile(float delay);
        protected abstract void MoveProjectile();
    }
}