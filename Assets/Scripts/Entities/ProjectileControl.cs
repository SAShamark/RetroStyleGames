using System.Collections;
using UnityEngine;

namespace Entities
{
    public abstract class ProjectileControl : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] protected float _moveSpeed = 3f;
        [SerializeField] private float _lifeTime = 5;
        private IEnumerator _turnOffProjectileCoroutine;

        private void Start()
        {
            _turnOffProjectileCoroutine = TurnOffProjectile(_lifeTime);
            StartCoroutine(_turnOffProjectileCoroutine);
        }

        private void Update()
        {
            Move();
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


        private void OnDisable()
        {
            if (_turnOffProjectileCoroutine != null)
            {
                StopCoroutine(_turnOffProjectileCoroutine);
            }
        }

        private IEnumerator TurnOffProjectile(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            gameObject.SetActive(false);
        }

        protected abstract void Move();
       
    }
}