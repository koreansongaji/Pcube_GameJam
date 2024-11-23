using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToTitleButtonClick()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
