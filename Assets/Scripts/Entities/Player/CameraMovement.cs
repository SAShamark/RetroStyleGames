using UI_InputSystem.Base;
using UnityEngine;

namespace Entities.Player
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;

        [SerializeField] private Transform _cameraTranform;


        [SerializeField] [Range(25f, 150f)] private float _mouseSensX = 75f;


        [SerializeField] [Range(25f, 150f)] private float _mouseSensY = 75f;

        [SerializeField] private float _minClampVertical = -60;

        [SerializeField] private float _maxClampHorizontal = 90;

        private float _verticalRotation;

        private float XValueWithSens => UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.CameraLook) * Time.deltaTime *
                                        _mouseSensX;

        private float YValueWithSens =>
            UIInputSystem.ME.GetAxisVertical(JoyStickAction.CameraLook) * Time.deltaTime * _mouseSensY;

        private float RotationClamped(float refRotation) =>
            Mathf.Clamp(refRotation, _minClampVertical, _maxClampHorizontal);

        private void FixedUpdate()
        {
            CameraHorizontalMovement();
            CameraVerticalMovement();
        }

        private void CameraVerticalMovement()
        {
            if (!_cameraTranform) return;

            _verticalRotation -= YValueWithSens;
            _verticalRotation = RotationClamped(_verticalRotation);

            _cameraTranform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void CameraHorizontalMovement()
        {
            if (_playerTransform == null) return;

            _playerTransform.Rotate(Vector3.up * XValueWithSens);
        }
    }
}