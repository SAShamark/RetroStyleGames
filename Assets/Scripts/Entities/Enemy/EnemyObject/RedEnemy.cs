using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy.EnemyObject
{
    public class RedEnemy : BaseEnemy
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _timeToFly = 2;

        private const float YUpPosition = 1;
        private const float FallibilityY = 0.1f;
        private const int CharacterLayer = 8;
        private bool _isMove;

        private void OnEnable()
        {
            _navMeshAgent.enabled = false;
            _isMove = false;
            StartCoroutine(BeforeMove());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == CharacterLayer)
            {
                var characterView = other.gameObject.GetComponent<CharacterController>();
                characterView.CharacterStatsControl.TakeDamage(Attack);
                Death();
            }
        }

        private void Update()
        {
            MoveToTarget();
        }

        private void OnDisable()
        {
            StopCoroutine(BeforeMove());
        }

        private IEnumerator BeforeMove()
        {
            while (transform.position.y <= YUpPosition - FallibilityY)
            {
                ComeUp();
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(_timeToFly);

            while (transform.position.y >= FallibilityY)
            {
                ComeDown();
                yield return new WaitForEndOfFrame();
            }

            var transformPosition = transform.position;
            transformPosition = new Vector3(transformPosition.x, 0, transformPosition.z);
            transform.position = transformPosition;
            _navMeshAgent.enabled = true;
            _isMove = true;
        }

        public override void ChangeTarget(Vector3 newTarget)
        {
        }

        private void ComeUp()
        {
            var position = transform.position;
            position = Vector3.Lerp(position, new Vector3(position.x, YUpPosition, position.z),
                MoveSpeed * Time.deltaTime);
            transform.position = position;
        }

        private void ComeDown()
        {
            var transformPosition = transform.position;
            transformPosition =
                Vector3.Lerp(transformPosition, new Vector3(transformPosition.x, 0, transformPosition.z),
                    MoveSpeed * Time.deltaTime);
            transform.position = transformPosition;
        }

        protected override void MoveToTarget()
        {
            if (_isMove)
            {
                base.MoveToTarget();
            }
        }
    }
}