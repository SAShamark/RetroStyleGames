using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Character.Data
{
    [Serializable]
    public class CharacterShootData
    {
        [SerializeField] private CharacterProjectile _characterProjectilePrefab;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _projectileContainer;
        [SerializeField] private Transform _character;
        [SerializeField] private Transform _camera;
        [SerializeField] private int _countProjectile;
        public CharacterProjectile CharacterProjectilePrefab => _characterProjectilePrefab;
        public Transform ProjectileStartPosition => _projectileStartPosition;
        public Transform ProjectileContainer => _projectileContainer;
        public Transform Character => _character;
        public Transform Camera => _camera;
        public int CountProjectile => _countProjectile;
    }
}