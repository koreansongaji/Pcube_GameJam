using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SoundManager;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    int a = 0;


    void Start()
    {
        SoundManager.Init();
        a = DataManager.Instance.LoadGameData().saveStruct.a;
        //GameObject.Find("Text").GetComponent<TextMeshProUGUI>().text = a + "";
        StateUnitManager.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //SoundManager.Instance.PlaySE(ESoundEffect.SE_Beat_01);
            a++;
            //GameObject.Find("Text").GetComponent<TextMeshProUGUI>().text = a+"";
            DataManager.Instance.SaveGameData(new Data(a));
        }
    }
}
