using System;
using UnityEngine;

namespace Entities.Character
{
    [Serializable]
    public class CharacterData
    {
        [SerializeField] private float _health;
        [Range(0, 100)] [SerializeField] private float _power;

        public float Health => _health;
        public float Power => _power;
    }
}