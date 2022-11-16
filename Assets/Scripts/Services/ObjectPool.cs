using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private T Prefab { get; }
        private Transform Container { get; }
        public List<T> Pool;

        public ObjectPool(T prefab, int count, Transform container)
        {
            Prefab = prefab;
            Container = container;

            CreatPool(count);
        }

        public ObjectPool(T prefab, int count)
        {
            Prefab = prefab;
            CreatPool(count);
        }

        private void CreatPool(int count)
        {
            Pool = new List<T>();
            for (int i = 0; i < count; i++)
            {
                CreatObject();
            }
        }

        private T CreatObject(bool isActiveByDefault = false)
        {
            var creatObject = Object.Instantiate(Prefab, Container);
            creatObject.gameObject.SetActive(isActiveByDefault);
            Pool.Add(creatObject);
            return creatObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (var mono in Pool.Where(mono => !mono.gameObject.activeSelf))
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }

            element = null;
            return element;
        }

        public T GetFreeElement()
        {
            return HasFreeElement(out var element) ? element : CreatObject(true);
        }
    }
}