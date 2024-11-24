using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Pause = !GameManager.Pause;
            if (GameManager.Pause)
            {
                OnPause();
            }
            else
            {
                ClosePause();
            }
        }
    }

    private void ClosePause()
    {
    }

    private void OnPause()
    {
        // todo : pause menu
    }
}
