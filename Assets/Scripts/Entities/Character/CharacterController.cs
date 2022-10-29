using System;
using Entities.Character.Abilities;
using Entities.Character.Data;
using UnityEngine;


namespace Entities.Character
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterStatsControl CharacterStatsControl { get; private set; }

        [SerializeField] [Range(2f, 10f)] private float _moveSpeed = 8;
        [SerializeField] private CharacterCameraData _characterCameraData;
        [SerializeField] private CharacterData _characterData;

        private CharacterMovement _characterMovement;
        private CharacterCameraMovement _characterCameraMovement;


        private void Start()
        {
            _characterMovement = new CharacterMovement(_moveSpeed, transform);
            _characterCameraMovement = new CharacterCameraMovement(_characterCameraData, transform);
            CharacterStatsControl = new CharacterStatsControl(_characterData);
        }

        private void FixedUpdate()
        {
            _characterMovement.MoveCharacter();
            _characterCameraMovement.CameraMovement();
        }
    }
}