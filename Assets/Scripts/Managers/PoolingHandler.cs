using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingHandler : MonoBehaviour //이 스크립트는 싱글톤으로 안만들고 스폰 지점에 게임 오브젝트에 적용시킨다
{
    public GameObject[] Prefabs; //몬스터 종류만 저장할 배열
    //0 : Mouse, 1 : Pigeon, 2 : Pudu, 3 : Hog
    public List<GameObject>[] PooledObject; //소환한 몬스터들을 저장할 배열
    public GameObject[] SpawnPoint;
    public List<GameObject>[] ActiveMonster;
    public List<GameObject>[] DeActiveMonster;

    public int[] CountMonster = new int[4];
    private int[] TimeIndex = new int[4];
    

    public struct SpawnMonsterInfo
    {
        public float time; //분
        public readonly int num; //스폰할 몬스터 수
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
    /// 풀링 함수
    /// </summary>
    private void Pooling()
    {
        PooledObject = new List<GameObject>[Prefabs.Length];
        ActiveMonster = new List<GameObject>[Prefabs.Length];
        DeActiveMonster = new List<GameObject>[Prefabs.Length];

        for (int index = 0; index < Prefabs.Length; index++)
        {
            PooledObject[index] = new List<GameObject>();
            ActiveMonster[index] = new List<GameObject>();
            DeActiveMonster[index] = new List<GameObject>();
        }
        InstantMonster(0, 100);
        InstantMonster(1, 100);
        InstantMonster(2, 100);
        InstantMonster(3, 100);
    }

    /// <summary>
    /// 풀링 시키기 위해 몬스터를 생성해서 비활성화 시킴
    /// </summary>
    /// <param name="index">생성할 몬스터 종류</param>
    /// <param name="count">생성할 몬스터 수</param>
    /// <returns></returns>
    private GameObject InstantMonster(int index, int count) //index의 몬스터를 count개수만큼 생성
    {
        GameObject select = null;
        for (int i = 0; i < count; i++)
        {
            select = Instantiate(Prefabs[index], transform);
            PooledObject[index].Add(select);
            DeActiveMonster[index].Add(select);
            select.SetActive(false);
        }

        return select;
    }

    /// <summary>
    /// 풀링시킨 몬스터를 활성화 시킴
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
    /// 풀링한 몬스터 활성화 시키는 함수
    /// </summary>
    /// <param name="monsterKind">어떤 종류의 몬스터를 활성화 시킬지 정함</param>
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
            ActiveMonster[monsterKind].Add(spawn);
            DeActiveMonster[monsterKind].Remove(spawn);
            CountMonster[monsterKind]++;
        }
        return spawn;
    }
}
