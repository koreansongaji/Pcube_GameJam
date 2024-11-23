using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour //이 스크립트는 싱글톤으로 안만들고 스폰 지점에 게임 오브젝트에 적용시킨다
{
    public GameObject[] Prefabs; //몬스터 종류만 저장할 배열
    public List<GameObject>[] Pools; //소환한 몬스터들을 저장할 배열
    public GameObject[] SpawnPoint;

    private int CountLevel_1 = 0;
    private int CountLevel_2 = 0;
    private int CountLevel_3 = 0;
    private int CountLevel_4 = 0;
    private int IndexLevel_1 = 0;
    private int IndexLevel_2 = 0;
    private int IndexLevel_3 = 0;
    private int IndexLevel_4 = 0;


    public struct SpawnMonsterInfo
    {
        public float time; //분
        public int num; //스폰할 몬스터 수
        public SpawnMonsterInfo(float t, int n)
        {
            this.time = t;
            this.num = n;
        }
    }


    private SpawnMonsterInfo[] Level_1_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(0.1f, 5),
        new SpawnMonsterInfo(0.5f, 10),
        new SpawnMonsterInfo(1f, 10),
    };


    private SpawnMonsterInfo[] Level_2_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(1f, 3),
    };
    private SpawnMonsterInfo[] Level_3_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(5f, 3),
    };
    private SpawnMonsterInfo[] Level_4_Time = new SpawnMonsterInfo[]
    {
        new SpawnMonsterInfo(10f, 1),
    };

    private void SetUp()
    {
        CountLevel_1 = 0;
        CountLevel_2 = 0;
        CountLevel_3 = 0;
        CountLevel_4 = 0;
        IndexLevel_1 = 0;
        IndexLevel_2 = 0;
        IndexLevel_3 = 0;
        IndexLevel_4 = 0;
    }
    private void Awake()
    {
        SetUp();
        Pooling();
    }
    void Start()
    {

    }

    void Update()
    {
        SpawnMonster();
    }

    /// <summary>
    /// 풀링 함수
    /// </summary>
    private void Pooling()
    {
        Pools = new List<GameObject>[Prefabs.Length];

        for (int index = 0; index < Prefabs.Length; index++)
        {
            Pools[index] = new List<GameObject>();
        }
        InstantMonster(0, 100);
        InstantMonster(1, 30);
        InstantMonster(2, 30);
        InstantMonster(3, 3);
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
            select = Instantiate(Prefabs[index], this.transform);
            Pools[index].Add(select);
            select.SetActive(false);
        }

        return select;
    }

    /// <summary>
    /// 풀링시킨 몬스터를 활성화 시킴
    /// </summary>
    private void SpawnMonster()
    {
        //if (GameManager.Instance != null && GameManager.Instance.time >= Level_1_Time[IndexLevel_1].time)
        {
            for (int i = 0; i < Level_1_Time[IndexLevel_1].num; i++)
            {
                SpawnLevel_1();
            }
            IndexLevel_1++;
        }
        //if (GameManager.Instance != null && GameManager.Instance.time >= Level_2_Time[IndexLevel_2].time)
        {
            for (int i = 0; i < Level_2_Time[IndexLevel_1].num; i++)
            {
                SpawnLevel_2();
            }
            IndexLevel_2++;
        }
        //if (GameManager.Instance != null && GameManager.Instance.time >= Level_3_Time[IndexLevel_3].time)
        {
            for (int i = 0; i < Level_3_Time[IndexLevel_1].num; i++)
            {
                SpawnLevel_3();
            }
            IndexLevel_3++;
        }
        //if (GameManager.Instance != null && GameManager.Instance.time >= Level_4_Time[IndexLevel_4].time)
        {
            for (int i = 0; i < Level_4_Time[IndexLevel_1].num; i++)
            {
                SpawnLevel_4();
            }
            IndexLevel_4++;
        }
    }

    /// <summary>
    /// 레벨 1 몬스터 활성화
    /// </summary>
    /// <returns></returns>
    private GameObject SpawnLevel_1()
    {
        GameObject spawn = null;
        int randomSpawn = 0;
        randomSpawn = Random.Range(0, SpawnPoint.Length);
        if (CountLevel_1 < Pools[0].Count)
        {
            spawn = Pools[0][CountLevel_1];
            spawn.transform.position = SpawnPosition(SpawnPoint[randomSpawn], spawn).position;
            spawn.SetActive(true);
            CountLevel_1++;
        }

        return spawn;
    }

    /// <summary>
    /// 레벨 2 몬스터 활성화
    /// </summary>
    /// <returns></returns>
    private GameObject SpawnLevel_2()
    {
        GameObject spawn = null;
        int randomSpawn = 0;
        randomSpawn = Random.Range(0, SpawnPoint.Length);
        if (CountLevel_2 < Pools[1].Count)
        {
            spawn = Pools[1][CountLevel_2];
            spawn.transform.position = SpawnPosition(SpawnPoint[randomSpawn], spawn).position;

            spawn.SetActive(true);
            CountLevel_2++;
        }
        return spawn;
    }

    /// <summary>
    /// 레벨 3 몬스터 활성화
    /// </summary>
    /// <returns></returns>
    private GameObject SpawnLevel_3()
    {
        GameObject spawn = null;
        int randomSpawn = 0;
        randomSpawn = Random.Range(0, SpawnPoint.Length);
        if (CountLevel_3 < Pools[2].Count)
        {
            spawn = Pools[2][CountLevel_3];
            spawn.transform.position = SpawnPosition(SpawnPoint[randomSpawn], spawn).position;

            spawn.SetActive(true);
            CountLevel_3++;
        }
        return spawn;
    }

    /// <summary>
    /// 레벨 4 몬스터 활성화
    /// </summary>
    /// <returns></returns>
    private GameObject SpawnLevel_4()
    {
        GameObject spawn = null;
        int randomSpawn = 0;
        randomSpawn = Random.Range(0, SpawnPoint.Length);
        if (CountLevel_4 < Pools[3].Count)
        {
            spawn = Pools[3][CountLevel_4];
            spawn.transform.position = SpawnPosition(SpawnPoint[randomSpawn], spawn).position;

            spawn.SetActive(true);
            CountLevel_4++;
        }
        return spawn;
    }

    /// <summary>
    /// 몬스터 스폰할 위치 반환해줌
    /// </summary>
    /// <param name="spawnObject">스폰 포인트 게임 오브젝트</param>
    /// <returns></returns>
    private Transform SpawnPosition(GameObject spawnObject, GameObject _spawn)
    {
        Transform spawn = _spawn.transform;
        RaycastHit hit;

        Debug.DrawRay(spawnObject.transform.position, Vector3.down * 500f, Color.magenta);
        if (Physics.Raycast(spawnObject.transform.position, Vector3.down, out hit))
        {
            Debug.Log("DOWN 스폰포인트 레이 감지");
            spawn.position = hit.point;
        }

        return spawn;
    }
}
