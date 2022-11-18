using UI_InputSystem.Base;
using UnityEngine;

namespace Entities.Character.Controls
{
    public class CharacterMovement 
    {
        private readonly Transform _characterTransform;
        private readonly float _moveSpeed;

        public CharacterMovement(float moveSpeed, Transform characterTransform)
        {
            _moveSpeed = moveSpeed;
            _characterTransform = characterTransform;
        }

        public void MoveCharacter()
        {
            _characterTransform.position += CharacterMovementDirection();
        }

        private Vector3 CharacterMovementDirection()
        {
            var direction =
                _characterTransform.right * UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.Movement) +
                _characterTransform.forward * UIInputSystem.ME.GetAxisVertical(JoyStickAction.Movement);

            direction *= _moveSpeed * Time.deltaTime;
            return direction;
        }
    }
}