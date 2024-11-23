using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// MonsterData�� �޾Ƽ� ���� �������� ��ȯ
/// </summary>
public class MonsterStatus : MonoBehaviour
{
    [SerializeField] private MonsterData monsterData;
    public MonsterData runtimeData;
    
    private void Awake()
    {
        if (monsterData == null)
        {
            Debug.LogError("MonsterData is null");
            Debug.Log(monsterData);
        }
        
        runtimeData = Instantiate<MonsterData>(monsterData);
    }
}
