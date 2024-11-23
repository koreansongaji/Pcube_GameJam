using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Generic
{
    public class GameObjectPool : IObjectPool<GameObject>
    {
        private readonly Stack<GameObject> _pool;
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        
        #if UNITY_EDITOR
        static int cnt = 0;
        #endif

        public GameObjectPool(GameObject prefab, Transform parent = null, int initialCapacity = 10)
        {
            _pool = new Stack<GameObject>(initialCapacity);
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialCapacity; i++)
            {
                var obj = CreateNewObject();
                _pool.Push(obj);
                obj.SetActive(false);
            }
        }

        public GameObject Get()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                return CreateNewObject();
            }
        }

        public PooledObject<GameObject> Get(out GameObject v)
        {
            v = Get();
            return new PooledObject<GameObject>(v, this);
        }

        public void Release(GameObject element)
        {
            if (element.activeSelf == false)
            {
                Debug.LogWarning("Element is already inactive");
                return;
            }
            
            element.SetActive(false);
            _pool.Push(element);
        }

        public void Clear()
        {
            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                Object.Destroy(obj);
            }
        }

        public int CountInactive => _pool.Count;

        
        private GameObject CreateNewObject()
        {
#if UNITY_EDITOR
            GameObject ret = Object.Instantiate(_prefab, _parent);
            ret.name = _prefab.name + cnt++;
            return ret;
#else
            return Object.Instantiate(_prefab, _parent);
#endif
        }
    }
}