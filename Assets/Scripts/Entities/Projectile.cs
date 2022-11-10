using System.Collections;
using UnityEngine;

namespace Entities
{
    public abstract class Projectile : MonoBehaviour
    {
        public float AttackValue { get; set; }
        public int KillCount { get; private set; }

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] protected float _moveSpeed = 3f;
        [SerializeField] protected float _lifeTime = 5;
        private IEnumerator _turnOffCoroutine;

        protected virtual void Update()
        {
            MoveProjectile();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            int triggerEnterCountLayer = ((1 << other.gameObject.layer) & _layerMask);

            if (triggerEnterCountLayer != 0)
            {
                StartCoroutine(_turnOffCoroutine);
            }
        }

        protected virtual void OnEnable()
        {
            _turnOffCoroutine = TurnOffProjectile(_lifeTime);
            StartCoroutine(_turnOffCoroutine);
        }

        protected virtual void OnDisable()
        {
            if (_turnOffCoroutine != null)
            {
                StopCoroutine(_turnOffCoroutine);
            }
        }

        protected void IncreaseKillCount()
        {
            KillCount++;
        }

        public void ResetKillCount()
        {
            KillCount = 0;
        }

        protected virtual void TurnOffProjectile()
        {
            ResetKillCount();
            gameObject.SetActive(false);
        }
        protected abstract IEnumerator TurnOffProjectile(float delay);
        protected abstract void MoveProjectile();
    }
}