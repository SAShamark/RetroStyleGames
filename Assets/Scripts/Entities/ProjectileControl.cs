using System.Collections;
using UnityEngine;

namespace Entities
{
    public abstract class ProjectileControl : MonoBehaviour
    {
        public float AttackValue { get; set; }
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] protected float _moveSpeed = 3f;
        [SerializeField] protected float _lifeTime = 5;
        protected IEnumerator TurnOffProjectileCoroutine;
        private bool _move = true;


        private void Update()
        {
            if (_move)
            {
                Move();
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var triggerEnterCountLayer = ((1 << other.gameObject.layer) & _layerMask);
            
            if (triggerEnterCountLayer != 0)
            {
                gameObject.SetActive(false);
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
        protected abstract void Move();
    }
}