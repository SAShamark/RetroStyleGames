using System;
using Entities;
using UnityEngine;

public class TeleportField : MonoBehaviour
{
    public event Action<Vector3> OnCharacterTeleported;
    private const int CharacterLayer = 8;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == CharacterLayer)
        {
            other.transform.position = PositionProcessor.GetNewPosition();
            OnCharacterTeleported?.Invoke(other.transform.position);
        }
    }
}