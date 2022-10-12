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
        protected IEnumerator _turnOffProjectileCoroutine;
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
            if (((1 << other.gameObject.layer) & _layerMask) != 0)
            {
                gameObject.SetActive(false);
                if (_turnOffProjectileCoroutine != null)
                {
                    StopCoroutine(_turnOffProjectileCoroutine);
                }
            }
        }

       

        protected virtual void OnDisable()
        {
            if (_turnOffProjectileCoroutine != null)
            {
                StopCoroutine(_turnOffProjectileCoroutine);
            }
        }

        protected abstract IEnumerator TurnOffProjectile(float lifeTime);
        

        protected abstract void Move();
    }
}