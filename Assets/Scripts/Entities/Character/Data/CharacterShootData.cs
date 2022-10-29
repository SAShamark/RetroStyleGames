using System;
using UnityEngine;

namespace Entities.Character.Data
{
    [Serializable]
    public class CharacterShootData
    {
        [SerializeField] private ProjectileControl _projectile;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private int _countProjectile;
        [SerializeField] private Transform _camera;
        public ProjectileControl Projectile => _projectile;
        public Transform Container => _container;
        public Transform ProjectileStartPosition => _projectileStartPosition;
        public int CountProjectile => _countProjectile;
        public Transform Camera => _camera;
    }
}