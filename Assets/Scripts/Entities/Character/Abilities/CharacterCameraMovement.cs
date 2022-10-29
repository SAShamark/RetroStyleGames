using Entities.Character.Data;
using UI_InputSystem.Base;
using UnityEngine;

namespace Entities.Character.Abilities
{
    public class CharacterCameraMovement
    {
        private readonly Transform _characterTransform;
        private readonly CharacterCameraData _characterCameraData;

        private float _verticalRotation;

        private float XValueWithSens => UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.CameraLook) * Time.deltaTime *
                                        _characterCameraData.MouseSensX;

        private float YValueWithSens =>
            UIInputSystem.ME.GetAxisVertical(JoyStickAction.CameraLook) * Time.deltaTime *
            _characterCameraData.MouseSensY;

        private float RotationClamped(float refRotation) =>
            Mathf.Clamp(refRotation, _characterCameraData.MinClampVertical, _characterCameraData.MaxClampHorizontal);

        public CharacterCameraMovement(CharacterCameraData characterCameraData, Transform characterTransform)
        {
            _characterCameraData = characterCameraData;
            _characterTransform = characterTransform;
        }


        public void CameraMovement()
        {
            CameraHorizontalMovement();
            CameraVerticalMovement();
        }

        private void CameraVerticalMovement()
        {
            if (!_characterCameraData.CameraTransform) return;

            _verticalRotation -= YValueWithSens;
            _verticalRotation = RotationClamped(_verticalRotation);

            _characterCameraData.CameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void CameraHorizontalMovement()
        {
            _characterTransform.Rotate(Vector3.up * XValueWithSens);
        }
    }
}