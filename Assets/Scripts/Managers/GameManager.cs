using System.Collections;
using System.Collections.Generic;
using Generic;
using TMPro;
using UnityEngine;
using static SoundManager;

public class GameManager : Singleton<GameManager>
{
    UIManager UIObject;
    private void Awake()
    {
        SoundManager.Init();
        UIObject = UIManager.Init();
        UIObject.OpenStateToggle();

    }
}
