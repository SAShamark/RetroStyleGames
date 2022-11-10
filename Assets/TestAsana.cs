using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TestAsana : MonoBehaviour
{
    private IEnumerator _enumerator1;
    private IEnumerator _enumerator2;
    [SerializeField] private Button _gugana;

    private void Start()
    {
        _enumerator1 = TestOne(3);
        _enumerator2 = TestOne(0);
        StartCoroutine(_enumerator1);
        _gugana.onClick.AddListener(ClickButtonYes);
    }

    private IEnumerator TestOne(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        print(delayTime);
    }

    private void ClickButtonYes()
    {
        StartCoroutine(_enumerator2);
    }

    private void OnDisable()
    {
        if (_enumerator1 != null)
        {
            StopCoroutine(_enumerator1);
        }

        if (_enumerator2 != null)
        {
            StopCoroutine(_enumerator2);
        }
    }
}