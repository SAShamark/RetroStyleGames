using System;
using UI_InputSystem.Base;
using UnityEngine;

namespace Entities.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        public event Action<Vector3> OnTelepot;

        private Transform _characterTransform;
        [SerializeField] [Range(2f, 10f)] private float _moveSpeed = 8;

        private const float MinCharacterYPosition = -1;

        private void Start()
        {
            _characterTransform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            if (_characterTransform.position.y > MinCharacterYPosition)
            {
                MoveCharacter();
            }
            else
            {
                TeleportCharacter();
            }
        }

        private void TeleportCharacter()
        {
            OnTelepot?.Invoke(_characterTransform.position);
            _characterTransform.position = PositionProcessor.GetNewPosition();
        }

        private void MoveCharacter()
        {
            _characterTransform.position += CharacterMovementDirection();
        }

        private Vector3 CharacterMovementDirection()
        {
            var baseDirection =
                _characterTransform.right * UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement) +
                _characterTransform.forward * UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement);

            baseDirection *= _moveSpeed * Time.deltaTime;
            return baseDirection;
        }
    }
}