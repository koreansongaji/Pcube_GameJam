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

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Ebgm.FALL);
    }
}
