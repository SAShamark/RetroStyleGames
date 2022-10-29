using Entities.Character.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Character
{
    public class CharacterFactory : EntitiesFactory
    {
        [SerializeField] private Transform _startCharacterPosition;
        [SerializeField] private CharacterController _characterControllerPrefab;
        
        
        
        public override void Start()
        {
            Spawn();
        }
        public override void Spawn()
        {
            var characterObject = Instantiate(_characterControllerPrefab, _startCharacterPosition);
        }
        
    }
}