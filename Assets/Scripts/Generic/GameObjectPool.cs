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
        private readonly bool _createNewObject;
        
        private int _activeCount;
        
#if UNITY_EDITOR
        private static int _debugNumber = 0;
#endif
        public GameObjectPool(GameObject prefab, Transform parent = null, int initialCapacity = 10, bool createNewObject = true)
        {
            _pool = new Stack<GameObject>(initialCapacity);
            _prefab = prefab;
            _parent = parent;
            _createNewObject = createNewObject;

            for (int i = 0; i < initialCapacity; i++)
            {
                var obj = CreateNewObject();
                _pool.Push(obj);
                obj.SetActive(false);
            }
            
            _activeCount = 0;
        }

        public GameObject Get(Transform parent)
        {
            var obj = Get();
            obj.transform.SetParent(parent);
            return obj;
        }
        
        public GameObject Get()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                obj.SetActive(true);
                _activeCount++;
                return obj;
            }
            else
            {
                if (_createNewObject)
                {
                    _activeCount++;
                    return CreateNewObject();
                }
                else
                {
                    Debug.LogWarning("Pool is empty");
                    return null;
                }
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
            
            _activeCount--;
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
        public int CountActive => _activeCount;

        
        private GameObject CreateNewObject()
        {
#if UNITY_EDITOR
            GameObject ret = Object.Instantiate(_prefab, _parent);
            ret.name = _prefab.name + _debugNumber++;
            return ret;
#else
            return Object.Instantiate(_prefab, _parent);
#endif
        }
    }
}