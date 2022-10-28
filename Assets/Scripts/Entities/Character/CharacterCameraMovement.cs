using System;
using UI_InputSystem.Base;
using UnityEngine;

namespace Entities.Character
{
    public class CharacterCameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        private Transform _playerTransform;
        [SerializeField] private CharacterCameraData _characterCameraData;

        private float _verticalRotation;

        private float XValueWithSens => UIInputSystem.ME.GetAxisHorizontal(JoyStickAction.CameraLook) * Time.deltaTime *
                                        _characterCameraData.MouseSensX;

        private float YValueWithSens =>
            UIInputSystem.ME.GetAxisVertical(JoyStickAction.CameraLook) * Time.deltaTime *
            _characterCameraData.MouseSensY;

        private float RotationClamped(float refRotation) =>
            Mathf.Clamp(refRotation, _characterCameraData.MinClampVertical, _characterCameraData.MaxClampHorizontal);

        private void Start()
        {
            _playerTransform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            CameraHorizontalMovement();
            CameraVerticalMovement();
        }

        private void CameraVerticalMovement()
        {
            if (!_cameraTransform) return;

            _verticalRotation -= YValueWithSens;
            _verticalRotation = RotationClamped(_verticalRotation);

            _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void CameraHorizontalMovement()
        {
            _playerTransform.Rotate(Vector3.up * XValueWithSens);
        }
    }

    [Serializable]
    public class CharacterCameraData
    {
        
        [SerializeField] [Range(25f, 150f)] private float _mouseSensX = 75f;
        [SerializeField] [Range(25f, 150f)] private float _mouseSensY = 75f;
        [SerializeField] private float _minClampVertical = -60;
        [SerializeField] private float _maxClampHorizontal = 90;

        public float MouseSensX => _mouseSensX;
        public float MouseSensY => _mouseSensY;
        public float MinClampVertical => _minClampVertical;
        public float MaxClampHorizontal => _maxClampHorizontal;
    }
}
