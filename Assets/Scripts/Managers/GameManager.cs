using System.Collections;
using System.Collections.Generic;
using Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundManager;

public class GameManager : Singleton<GameManager>
{
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
        Pause = false;
        cursorCoroutine = StartCoroutine(CursorCoroutineFunction());
    }

    private Coroutine cursorCoroutine;
    private GameObject cursorBuffer;

    private IEnumerator CursorCoroutineFunction()
    {
        yield return null;
        // Input.mousePosition
        cursorBuffer = Instantiate(
            Resources.Load("UIPrefab/cursor") as GameObject,
            Vector3.zero,
            Quaternion.identity,
            GameObject.Find("Canvas").transform
            );
        
        while (cursorBuffer != null)
        {
            cursorBuffer.transform.position = Input.mousePosition;
            yield return null;
        }
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
            Pause = true;
            StopCoroutine(cursorCoroutine);
            Destroy(cursorBuffer);
            SceneManager.LoadScene("GameClear");
        }
    }
    
    public bool TryGetPlayerObject(out Player player)
    {
        if (_player == null)
        {
            player = FindObjectOfType<Player>();
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