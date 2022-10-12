using System;
using UnityEngine;
using UnityEngine.UI;

public class UltaButton : MonoBehaviour
{
    [SerializeField] private Button _fireButton;
    public static event Action OnUlta;

    private void Start()
    {
        _fireButton.onClick.AddListener(Ulta);
    }

    private void OnDestroy()
    {
        _fireButton.onClick.RemoveListener(Ulta);
    }

    private void Ulta()
    {
        OnUlta?.Invoke();
    }

    public void Interactable(bool isActive)
    {
        _fireButton.interactable = isActive;
    }
}