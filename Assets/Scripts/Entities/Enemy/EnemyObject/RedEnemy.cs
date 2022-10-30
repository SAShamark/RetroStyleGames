using System.Collections;
using Entities.Character;
using UnityEngine;
using Zenject;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy.EnemyObject
{
    public class RedEnemy : BaseEnemy
    {
        [SerializeField] private Transform _model;
        private const float TimeToFly = 2;
        private const float YUpPosition = 1;
        private bool _move;
        private const float FallibilityY = 0.1f;
        private const int CharacterLayer = 8;

        private IEnumerator _startMoveCoroutine;
       
        private void Start()
        {
            _startMoveCoroutine = StartMove();
            StartCoroutine(_startMoveCoroutine);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == CharacterLayer)
            {
                var characterView = other.gameObject.GetComponent<CharacterController>();
                characterView.CharacterStatsControl.DecreaseHealth(Attack);
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            MoveToTarget();
        }

        private IEnumerator StartMove()
        {
            while (_model.position.y <= YUpPosition - FallibilityY)
            {
                var position = _model.position;
                position = Vector3.Lerp(position, new Vector3(position.x, YUpPosition, position.z),
                    MoveSpeed * Time.deltaTime);
                _model.position = position;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(TimeToFly);
            while (_model.localPosition.y >= FallibilityY)
            {
                _model.localPosition =
                    Vector3.Lerp(_model.localPosition, new Vector3(0, 0, 0), MoveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            _model.localPosition = new Vector3(0, 0, 0);

            _move = true;
        }

        private void OnDestroy()
        {
            if (_startMoveCoroutine != null)
            {
                StopCoroutine(_startMoveCoroutine);
            }
        }

        public override void ChangeTarget(Vector3 newTarget)
        {
        }

        protected override void MoveToTarget()
        {
            if (_move)
            {
                base.MoveToTarget();
            }
        }
    }
}