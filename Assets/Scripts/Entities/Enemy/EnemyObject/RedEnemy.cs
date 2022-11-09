using System.Collections;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy.EnemyObject
{
    public class RedEnemy : BaseEnemy
    {
        [SerializeField] private Transform _model;
        [SerializeField] private float _timeToFly = 2;
        
        private const float YUpPosition = 1;
        private const float FallibilityY = 0.1f;
        private const int CharacterLayer = 8;
        private bool _isMove;

        private IEnumerator _beforeMoveCoroutine;

        private void Start()
        {
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
            while (_model.position.y <= YUpPosition - FallibilityY)
            {
                ComeUp();
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(_timeToFly);
            
            while (_model.localPosition.y >= FallibilityY)
            {
                ComeDown();
                yield return new WaitForEndOfFrame();
            }

            _model.localPosition = new Vector3(0, 0, 0);

            _isMove = true;
        }

        public override void ChangeTarget(Vector3 newTarget)
        {
        }

        private void ComeUp()
        {
            var position = _model.position;
            position = Vector3.Lerp(position, new Vector3(position.x, YUpPosition, position.z),
                MoveSpeed * Time.deltaTime);
            _model.position = position;
        }

        private void ComeDown()
        {
            _model.localPosition =
                Vector3.Lerp(_model.localPosition, new Vector3(0, 0, 0), MoveSpeed * Time.deltaTime);
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