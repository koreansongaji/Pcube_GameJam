using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider HPBar;
    [SerializeField] Slider ExpBar;
    int statePoint;

    public void OpenStatToggle()
    {

    }
    public void SetHPBar(float a)
    {
        HPBar.value = a;
    }
    public void SetExpBar(float a)
    {
        ExpBar.value = a;
    }
}
