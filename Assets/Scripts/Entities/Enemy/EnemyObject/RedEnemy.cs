using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy.EnemyObject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RedEnemy : BaseEnemy
    {
        [SerializeField] private float _timeToFly = 2;

        private const float YUpPosition = 1;
        private const float FallibilityY = 0.1f;
        private const int CharacterLayer = 8;
        private bool _isMove;
        private IEnumerator _beforeMoveCoroutine;
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _beforeMoveCoroutine = BeforeMove();
            StartCoroutine(_beforeMoveCoroutine);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == CharacterLayer)
            {
                var characterView = other.gameObject.GetComponent<CharacterController>();
                characterView.CharacterStatsControl.DecreaseHealth(Attack);
                DecreaseHealth(Health);
            }
        }

        private void Update()
        {
            MoveToTarget();
        }

        private void OnDestroy()
        {
            if (_beforeMoveCoroutine != null)
            {
                StopCoroutine(_beforeMoveCoroutine);
            }
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

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
            transform.position =
                Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z),
                    MoveSpeed * Time.deltaTime);
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