using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameStartButtonClick()
    {
        GameManager.Instance.StartGame();
    }
    
    public void GameQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Ebgm.FALL);
    }
}
