using Entities.Character;
using UnityEngine;
using CharacterController = Entities.Character.CharacterController;

public 
    interface ICharacterView
{
}


public class CharacterView : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    [SerializeField] [Range(2f, 10f)] private float _moveSpeed = 8;

    private CharacterCameraMovement _characterCameraMovement;
    [SerializeField] private CharacterCameraData _characterCameraData;

    private CharacterController _characterController;
    [SerializeField] private CharacterData _characterData;

    private void Start()
    {
        _characterMovement = new CharacterMovement(_moveSpeed, transform);
        _characterCameraMovement = new CharacterCameraMovement(_characterCameraData, transform);
        _characterController = new CharacterController(_characterData.Health, _characterData.Power);
    }

    private void FixedUpdate()
    {
        _characterMovement.MoveCharacter();
        _characterCameraMovement.CameraMovement();
    }
}