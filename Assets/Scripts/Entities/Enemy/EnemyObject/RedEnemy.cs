using System;
using System.Collections;
using Entities.Player;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

namespace Entities.Enemy.EnemyObject
{
    public class RedEnemy : BaseEnemy
    {
        [SerializeField] private Transform _model;

        private float _timeToFly = 2;
        private float _yUpPosition = 1;
        private bool _move;
        private float _fallibilityY = 0.1f;
        private const int PlayerLayer = 8;

        private IEnumerator _startMoveCorutine;

        protected override void Start()
        {
            base.Start();
            _startMoveCorutine = StartMove();
            StartCoroutine(_startMoveCorutine);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == PlayerLayer)
            {
                var playerController = other.gameObject.GetComponentInChildren<CharacterController>();
                playerController.DecreaseHealth(Attack);
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            MoveToTarget();
        }

        private IEnumerator StartMove()
        {
            while (_model.position.y <= _yUpPosition - _fallibilityY)
            {
                var position = _model.position;
                position = Vector3.Lerp(position, new Vector3(position.x, _yUpPosition, position.z),
                    MoveSpeed * Time.deltaTime);
                _model.position = position;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(_timeToFly);
            while (_model.localPosition.y >= _fallibilityY)
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
            if (_startMoveCorutine != null)
            {
                StopCoroutine(_startMoveCorutine);
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