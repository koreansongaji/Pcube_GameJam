using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpReader : MonoBehaviour
{
    public Slider hpSlider;

    private float curValue;
    private float targetValue;

    public Slider HpSlider
    {
        get => hpSlider;
        set => hpSlider = value;
    }

    private void Update()
    {
        if (GameManager.Pause) return;
        GameManager.Instance.TryGetPlayerObject(out Player player);
        
        curValue = hpSlider.value;   
        targetValue = player.GetHealth() / player.GetStat().maxHp.Value;

        if (Math.Abs(curValue - targetValue) > 0.01f)
        {
            hpSlider.value = Mathf.Lerp(curValue, targetValue, 5 * Time.deltaTime);
        }
    }
}
