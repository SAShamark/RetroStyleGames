using System;
using UnityEngine;

namespace Entities.Character
{
    [Serializable]
    public class CharacterCameraData
    {
        [SerializeField] private Transform _cameraTransform;

        [SerializeField] [Range(25f, 150f)] private float _mouseSensX = 75f;
        [SerializeField] [Range(25f, 150f)] private float _mouseSensY = 75f;
        [SerializeField] private float _minClampVertical = -60;
        [SerializeField] private float _maxClampHorizontal = 90;
        
        public Transform CameraTransform => _cameraTransform;
        public float MouseSensX => _mouseSensX;
        public float MouseSensY => _mouseSensY;
        public float MinClampVertical => _minClampVertical;
        public float MaxClampHorizontal => _maxClampHorizontal;
    }
}