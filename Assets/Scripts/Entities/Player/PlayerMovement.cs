using UI_InputSystem.Base;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _groundChecker;

        [SerializeField] private CharacterController _controllerPlayer;

        [SerializeField] [Range(2f, 10f)] private float _playerHorizontalSpeed = 8;

        [SerializeField] private bool _useGravity = true;

        [SerializeField] [Range(-50f, -9.8f)] private float _gravityValue = -10;

        [SerializeField] [Range(0.1f, 1f)] private float _groundDistance = 0.5f;

        [SerializeField] private LayerMask _groundMask;

        private Vector3 gravityVelocity;
        private float _minPlayerYPosition = -1;

        private bool Grounded => Physics.CheckSphere(_groundChecker.position, _groundDistance, _groundMask);

        private void FixedUpdate()
        {
            if (_playerTransform.position.y > _minPlayerYPosition)
            {
                MovePlayer();
            }
            else
            {
                _playerTransform.position = PositionProcessor.GetNewPosition();
            }

            CalculateGravity();
        }

        private void MovePlayer()
        {
            if (!_playerTransform) return;

            _controllerPlayer.Move(PlayerMovementDirection());
        }

        private void CalculateGravity()
        {
            if (!_useGravity) return;
            if (!_groundChecker) return;

            ResetGravityIfGrounded();
            ApplyGravity();
        }


        private void ApplyGravity()
        {
            gravityVelocity.y += _gravityValue * Time.deltaTime;
            _controllerPlayer.Move(gravityVelocity * Time.deltaTime);
        }

        private void ResetGravityIfGrounded()
        {
            if (Grounded && gravityVelocity.y < 0)
                gravityVelocity.y = -1.5f;
        }

        private Vector3 PlayerMovementDirection()
        {
            var baseDirection = _playerTransform.right * UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement) +
                                _playerTransform.forward * UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement);

            baseDirection *= _playerHorizontalSpeed * Time.deltaTime;
            return baseDirection;
        }
    }
}