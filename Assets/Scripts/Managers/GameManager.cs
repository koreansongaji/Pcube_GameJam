using System.Collections;
using System.Collections.Generic;
using Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundManager;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] GameObject expPrefab;
    public Transform ExpPoolTransform;
    public Transform ExpPoolTransformSub;
    public Player player;
    public GameObjectPool _expPool;
    protected override void Awake()
    {
        base.Awake();
        SoundManager.Init();
        StartGame();
    }
    public float GameTime { get; private set; }

    

    public void StartGame()
    {
        // 1. Scene Change
        //SceneManager.LoadScene($"GameScene");
        _expPool = new GameObjectPool(expPrefab, ExpPoolTransform, 50);
        // 2. Game Start
        GameTime = 0;

        for(int i=0; i<40; i++)
        {
            GameObject a = _expPool.Get();
            a.transform.position = new Vector3(Random.value*10, 0.5f, Random.value * 10);
            a.transform.parent = ExpPoolTransformSub;
        }
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }
    
}
