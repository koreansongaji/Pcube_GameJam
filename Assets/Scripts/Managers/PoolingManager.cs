using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingManager : MonoBehaviour //�� ��ũ��Ʈ�� �̱������� �ȸ���� ���� ������ ���� ������Ʈ�� �����Ų��
{
    public GameObject[] Prefabs; //���� ������ ������ �迭
    //0 : Mouse, 1 : Pigeon, 2 : Pudu, 3 : Hog
    public List<GameObject>[] PooledObject; //��ȯ�� ���͵��� ������ �迭
    public GameObject[] SpawnPoint;

    private int[] CountMonster = new int[4];
    private int[] TimeIndex = new int[4];

    public struct SpawnMonsterInfo
    {
        public float time; //��
        public readonly int num; //������ ���� ��
        public SpawnMonsterInfo(float t, int n)
        {
            this.time = t;
            this.num = n;
        }
    }

    private SpawnMonsterInfo[] Level_1_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(5f, 5),
        new SpawnMonsterInfo(10f, 10),
        new SpawnMonsterInfo(12f, 10),
    };


    private SpawnMonsterInfo[] Level_2_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(3f, 3),
    };
    private SpawnMonsterInfo[] Level_3_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(4f, 3),
    };
    private SpawnMonsterInfo[] Level_4_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(7f, 1),
    };

    private void SetUp()
    {
        CountMonster[0] = 0;
        CountMonster[1] = 0;
        CountMonster[2] = 0;
        CountMonster[3] = 0;

        TimeIndex[0] = 0;
        TimeIndex[1] = 0;
        TimeIndex[2] = 0;
        TimeIndex[3] = 0;
    }
    private void Awake()
    {
        SetUp();
        Pooling();
    }

    void Update()
    {
        //SpawnMonsterTest();
        SpawnMonster();
    }

    /// <summary>
    /// Ǯ�� �Լ�
    /// </summary>
    private void Pooling()
    {
        PooledObject = new List<GameObject>[Prefabs.Length];

        for (int index = 0; index < Prefabs.Length; index++)
        {
            PooledObject[index] = new List<GameObject>();
        }
        InstantMonster(0, 100);
        InstantMonster(1, 100);
        InstantMonster(2, 100);
        InstantMonster(3, 100);
    }

    /// <summary>
    /// Ǯ�� ��Ű�� ���� ���͸� �����ؼ� ��Ȱ��ȭ ��Ŵ
    /// </summary>
    /// <param name="index">������ ���� ����</param>
    /// <param name="count">������ ���� ��</param>
    /// <returns></returns>
    private GameObject InstantMonster(int index, int count) //index�� ���͸� count������ŭ ����
    {
        GameObject select = null;
        for (int i = 0; i < count; i++)
        {
            select = Instantiate(Prefabs[index], this.transform);
            PooledObject[index].Add(select);
            select.SetActive(false);
        }

        return select;
    }

    /// <summary>
    /// Ǯ����Ų ���͸� Ȱ��ȭ ��Ŵ
    /// </summary>
    private void SpawnMonster()
    {
        if (TimeIndex[0] < Level_1_Time.Length)
        {
            if (GameManager.Instance.GameTime >= Level_1_Time[TimeIndex[0]].time)
            {
                for (int i = 0; i < Level_1_Time[TimeIndex[0]].num; i++)
                {
                    SetActiveMonster(0);
                }
                TimeIndex[0]++;
            }
        }
        if (TimeIndex[1] < Level_2_Time.Length)
        {
            if (GameManager.Instance.GameTime >= Level_2_Time[TimeIndex[1]].time)
            {
                for (int i = 0; i < Level_2_Time[TimeIndex[1]].num; i++)
                {
                    SetActiveMonster(1);
                }
                TimeIndex[1]++;
            }
        }

        if (TimeIndex[2] < Level_3_Time.Length)
        {
            if (GameManager.Instance.GameTime >= Level_3_Time[TimeIndex[2]].time)
            {
                for (int i = 0; i < Level_3_Time[TimeIndex[2]].num; i++)
                {
                    SetActiveMonster(2);
                }
                TimeIndex[2]++;
            }
        }
        if (TimeIndex[3] < Level_4_Time.Length)
        {
            if (GameManager.Instance.GameTime >= Level_4_Time[TimeIndex[3]].time)
            {
                for (int i = 0; i < Level_4_Time[TimeIndex[3]].num; i++)
                {
                    SetActiveMonster(3);
                }
                TimeIndex[3]++;
            }
        }
    }
    
    private void SpawnMonsterTest()
    {
        for (int i = 0; i < Level_1_Time[TimeIndex[0]].num; i++)
        {
            SetActiveMonster(0);
        }
        TimeIndex[0]++;
    }
    
    /// <summary>
    /// Ǯ���� ���� Ȱ��ȭ ��Ű�� �Լ�
    /// </summary>
    /// <param name="monsterKind">� ������ ���͸� Ȱ��ȭ ��ų�� ����</param>
    /// <returns></returns>
    private GameObject SetActiveMonster(int monsterKind)
    {
        GameObject spawn = null;
        int randomSpawn = 0;
        randomSpawn = UnityEngine.Random.Range(0, SpawnPoint.Length);
        if (CountMonster[monsterKind] < PooledObject[monsterKind].Count)
        {
            spawn = PooledObject[monsterKind][CountMonster[monsterKind]];
            spawn.transform.position = SpawnPoint[randomSpawn].transform.position;
            spawn.SetActive(true);
            CountMonster[monsterKind]++;
        }
        return spawn;
    }
}
