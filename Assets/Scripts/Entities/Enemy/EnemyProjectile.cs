using System.Collections;
using UnityEngine;

namespace Entities.Enemy
{
    public sealed class EnemyProjectile : Projectile
    {
        public Transform Target { get; set; }
        private Vector3 _lastTargetPosition;
        private bool _loseTarget;
        private const int PlayerLayer = 8;
        private IEnumerator _turnOffCoroutine;

        private void Start()
        {
            transform.parent = transform.parent.parent;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (other.gameObject.layer == PlayerLayer)
            {
                var playerController = other.gameObject.GetComponentInChildren<Character.CharacterController>();
                playerController.CharacterStatsControl.DecreasePower(AttackValue);
                TurnOffProjectile();
            }
        }


        protected override void OnDisable()
        {
            base.OnDisable();
            _loseTarget = false;
        }

        protected override IEnumerator TurnOffProjectile(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        public void ChangeTargetPosition(Vector3 lastPosition)
        {
            _loseTarget = true;
            _lastTargetPosition = lastPosition;
        }

        protected override void MoveProjectile()
        {
            var newPosition = Target.position;
            if (_loseTarget)
            {
                if (Vector3.Distance(transform.position, _lastTargetPosition) <= 0.2f)
                {
                    TurnOffProjectile();
                    return;
                }

                newPosition = _lastTargetPosition;
            }

            var transformPosition = transform.position;
            transformPosition += (newPosition - transformPosition).normalized * (_moveSpeed * Time.deltaTime);
            transform.position = transformPosition;
        }
    }
}