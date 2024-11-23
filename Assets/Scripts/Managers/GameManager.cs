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
    
    private Player _player;
    protected override void Awake()
    {
        base.Awake();
        SoundManager.Init();
    }
    public float GameTime { get; private set; }

    

    public void StartGame()
    {
        // 1. Scene Change
        //SceneManager.LoadScene($"GameScene");
        // 2. Game Start
        GameTime = 0;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }
    
    public bool TryGetPlayerObject(out Player player)
    {
        if (_player == null)
        {
            player = GameObject.FindObjectOfType<Player>();
            _player = player;
        }
        else
        {
            player = _player;
        }

        return player != null;
    }

    public static bool Pause
    {
        get => Time.timeScale == 0;
        set => Time.timeScale = value ? 0 : 1;
    }
}
