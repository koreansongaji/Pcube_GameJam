using System;
using System.Collections;
using System.Collections.Generic;
using Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundManager;

public class GameManager : Singleton<GameManager>
{
    public float GameTime { get; private set; }

    public void StartGame()
    {
        // 1. Scene Change
        SceneManager.LoadScene($"GameScene");
        
        // 2. Game Start
        GameTime = 0;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }
    
}
