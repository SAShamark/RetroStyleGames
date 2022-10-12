using System.Collections.Generic;
using UnityEngine;



public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public Transform Container { get; }
    private List<T> _pool;

    public ObjectPool(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;

        CreatPool(count);
    }

    private void CreatPool(int count)
    {
        _pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreatObject();
        }
    }

    private T CreatObject(bool isActiveByDefault = false)
    {
        var creatObject = Object.Instantiate(Prefab, Container);
        creatObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(creatObject);
        return creatObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in _pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return element;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        return CreatObject(true);
    }
}