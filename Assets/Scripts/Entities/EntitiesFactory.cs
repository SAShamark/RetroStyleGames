using Entities.Character;
using UnityEngine;

namespace Entities
{
    public abstract class EntitiesFactory : MonoBehaviour
    {
        protected Character.CharacterController CharacterController;
        public abstract void Start();
        public abstract void Spawn();
    }
}