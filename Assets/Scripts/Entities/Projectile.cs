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
        protected IEnumerator TurnOffProjectileDelay;
        protected IEnumerator TurnOffProjectileNow;

        protected virtual void Start()
        {
            TurnOffProjectileDelay = TurnOffProjectile(_lifeTime);
            TurnOffProjectileNow = TurnOffProjectile(0);
            StartCoroutine(TurnOffProjectileDelay);
        }

        protected virtual void Update()
        {
            MoveProjectile();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            int triggerEnterCountLayer = ((1 << other.gameObject.layer) & _layerMask);

            if (triggerEnterCountLayer != 0)
            {
                Debug.Log("enter");
                StartCoroutine(TurnOffProjectileNow);
            }
        }

        protected virtual void OnEnable()
        {
            if (TurnOffProjectileDelay != null)
            {
                StartCoroutine(TurnOffProjectileDelay);
            }
        }


        protected virtual void OnDisable()
        {
            if (TurnOffProjectileDelay != null)
            {
                StopCoroutine(TurnOffProjectileDelay);
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

        protected abstract IEnumerator TurnOffProjectile(float delay);
        protected abstract void MoveProjectile();
    }
}