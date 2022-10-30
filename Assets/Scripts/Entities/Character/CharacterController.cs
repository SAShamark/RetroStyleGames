using Entities.Character.Abilities;
using Entities.Character.Data;
using UI;
using UnityEngine;


namespace Entities.Character
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterStatsControl CharacterStatsControl { get; private set; }

        [SerializeField] [Range(2f, 10f)] private float _moveSpeed = 8;
        [SerializeField] private CharacterCameraData _characterCameraData;
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private CharacterShootData _characterShootData;

        private CharacterMovement _characterMovement;
        private CharacterCameraMovement _characterCameraMovement;
        private UltimateSkill _ultimateSkill;
        private ShootingCharacter _shootingCharacter;

        private void Start()
        {
            CharacterStatsControl = new CharacterStatsControl(_characterData);
            _characterMovement = new CharacterMovement(_moveSpeed, transform);
            _characterCameraMovement = new CharacterCameraMovement(_characterCameraData, transform);
            _ultimateSkill = new UltimateSkill();
            _shootingCharacter = new ShootingCharacter(_characterShootData, CharacterStatsControl);

            GamePanelView.OnUltimateSkill += _ultimateSkill.UseSkill;
            GamePanelView.OnShoot += _shootingCharacter.GetProjectile;
        }
        
        private void FixedUpdate()
        {
            _characterMovement.MoveCharacter();
            _characterCameraMovement.CameraMovement();
        }

        private void OnDestroy()
        {
            GamePanelView.OnUltimateSkill -= _ultimateSkill.UseSkill;
            GamePanelView.OnShoot -= _shootingCharacter.GetProjectile;
        }

        
    }
}