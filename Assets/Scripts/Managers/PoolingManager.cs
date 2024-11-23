using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour //�� ��ũ��Ʈ�� �̱������� �ȸ���� ���� ������ ���� ������Ʈ�� �����Ų��
{
    public GameObject[] Prefabs; //���� ������ ������ �迭
    public List<GameObject>[] Pools; //��ȯ�� ���͵��� ������ �迭
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
        public float time; //��
        public int num; //������ ���� ��
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
    /// Ǯ�� �Լ�
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
            Pools[index].Add(select);
            select.SetActive(false);
        }

        return select;
    }

    /// <summary>
    /// Ǯ����Ų ���͸� Ȱ��ȭ ��Ŵ
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
    /// ���� 1 ���� Ȱ��ȭ
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
    /// ���� 2 ���� Ȱ��ȭ
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
    /// ���� 3 ���� Ȱ��ȭ
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
    /// ���� 4 ���� Ȱ��ȭ
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
    /// ���� ������ ��ġ ��ȯ����
    /// </summary>
    /// <param name="spawnObject">���� ����Ʈ ���� ������Ʈ</param>
    /// <returns></returns>
    private Transform SpawnPosition(GameObject spawnObject, GameObject _spawn)
    {
        Transform spawn = _spawn.transform;
        RaycastHit hit;

        Debug.DrawRay(spawnObject.transform.position, Vector3.down * 500f, Color.magenta);
        if (Physics.Raycast(spawnObject.transform.position, Vector3.down, out hit))
        {
            Debug.Log("DOWN ��������Ʈ ���� ����");
            spawn.position = hit.point;
        }

        return spawn;
    }
}
