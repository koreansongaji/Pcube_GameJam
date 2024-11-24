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
    public float GameTime { get; private set; }
    
    [SerializeField] private float endTime = 60 * 10f;

    public float GetEndTime()
    {
        return endTime;
    }

    public void StartGame()
    {
        // 1. Scene Change
        SceneManager.LoadScene("InGame2");
        // 2. Game Start
        GameTime = 0;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "InGame2")
        {
            GameTime += Time.deltaTime;
        }
        
        CheckSufficientTime();
    }

    //시간이 다찼는지 확인하는 함수
    public void CheckSufficientTime()
    {
        if (GameTime > endTime)
        {
            GameTime = 0;
            SceneManager.LoadScene("GameClear");
        }
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
