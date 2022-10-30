using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Character.Data
{
    [Serializable]
    public class CharacterShootData
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _character;
        [SerializeField] private Transform _camera;
        [SerializeField] private int _countProjectile;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public Transform ProjectileStartPosition => _projectileStartPosition;
        public Transform Camera => _camera;
        public Transform Character => _character;
        public int CountProjectile => _countProjectile;
    }
}