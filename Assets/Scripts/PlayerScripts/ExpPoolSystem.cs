using Generic;
using UnityEngine;

public class ExpPoolSystem : MonoBehaviour
{
    public static ExpPoolSystem Instance
    {
        get;
        private set;
    }

    private GameObjectPool _expPool;
    
    [SerializeField] private GameObject expPrefab;

    private void Awake()
    {
        Instance = this;
        _expPool = new GameObjectPool(expPrefab, transform, 50, false);
    }
    
    private void OnDestroy()
    {
        Instance = null;
    }
    
    public void CreateExpSphere(Vector3 pos)
    {
        GameObject exp = _expPool.Get();
        if(exp == null)
            return;
        
        exp.transform.position = pos;
    }

    public void ReturnExpSphere(GameObject o)
    {
        _expPool.Release(o);
    }
}