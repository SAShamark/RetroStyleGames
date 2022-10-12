using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _health;
    [Range(0, 100)] [SerializeField] private float _power;
    private float _maxPower = 100;
    
    
}