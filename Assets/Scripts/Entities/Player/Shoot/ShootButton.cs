using System;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.Player.Shoot
{
    public class ShootButton : MonoBehaviour
    {
        [SerializeField] private Button _fireButton;
        public static event Action OnShoot;
        private void Start()
        {
            _fireButton.onClick.AddListener(Shoot);
        }

        private void OnDestroy()
        {
            _fireButton.onClick.RemoveListener(Shoot);
        }

        private void Shoot()
        {
            OnShoot?.Invoke(); 
        }
    }
}
