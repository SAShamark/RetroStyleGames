using System.Collections;
using UnityEngine;

namespace Entities.Enemy
{
    public class ProjectileControlEnemy : ProjectileControl
    {
        public Transform Target { get; set; }
        private Vector3 _lastTargetPosition;
        private bool _loseTarget;
        private const int PlayerLayer = 8;
        private Transform _startParent;

        private void Start()
        {
            
            _startParent = transform.parent;
            _turnOffProjectileCoroutine = TurnOffProjectile(_lifeTime);
            StartCoroutine(_turnOffProjectileCoroutine);
            transform.parent = null;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == PlayerLayer)
            {
                var playerController = other.gameObject.GetComponentInChildren<PlayerController>();
                playerController.DecreasePower(AttackValue);
                TurnOffGameObject();
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _loseTarget = false;
        }

        protected override IEnumerator TurnOffProjectile(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            transform.parent = _startParent;
            gameObject.SetActive(false);
        }

        public void ChangeTargetPosition(Vector3 lastPosition)
        {
            _loseTarget = true;
            _lastTargetPosition = lastPosition;
        }

        protected override void Move()
        {
            var newPosition = Target.position;
            if (_loseTarget)
            {
                if (Vector3.Distance(transform.position, _lastTargetPosition) <= 0.2f)
                {
                    TurnOffGameObject();
                    return;
                }

                newPosition = _lastTargetPosition;
            }

            transform.position += (newPosition - transform.position).normalized * _moveSpeed * Time.deltaTime;
        }

        private void TurnOffGameObject()
        {
            gameObject.SetActive(false);
            transform.parent = _startParent;
            StopCoroutine(_turnOffProjectileCoroutine);
        }
    }
}