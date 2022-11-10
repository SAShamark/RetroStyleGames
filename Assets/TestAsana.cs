using System;
using System.Collections;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class TestAsana : MonoBehaviour
{
    private IEnumerator _enumerator1;
    [SerializeField] private Button _gugana;

    private void Start()
    {
        _gugana.onClick.AddListener(ClickButtonYes);
    }

    private IEnumerator TestOne(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        print(delayTime);
    }

    private void ClickButtonYes()
    {
        StartCoroutine(_enumerator1);
    }

    private void OnEnable()
    {
        _enumerator1 = TestOne(3);
        StartCoroutine(_enumerator1);
    }

    private void OnDisable()
    {
        if (_enumerator1 != null)
        {
            StopCoroutine(_enumerator1);
        }
    }
}