using System;
using UI_InputSystem.Base;
using UnityEngine;

namespace Entities.Character.Abilities
{
    public class CharacterMovement
    {
        public event Action<Vector3> OnTelepot;

        private readonly Transform _characterTransform;
        private readonly float _moveSpeed;
        private const float MinCharacterYPosition = -1;

        public CharacterMovement(float moveSpeed,Transform characterTransform)
        {
            _moveSpeed = moveSpeed;
            _characterTransform = characterTransform;
        }


        public void FixedUpdate()
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

        public void MoveCharacter()
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