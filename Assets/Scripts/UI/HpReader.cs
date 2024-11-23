using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpReader : MonoBehaviour
{
    public Slider hpSlider;

    private void Update()
    {
        if (GameManager.Pause) return;
        GameManager.Instance.TryGetPlayerObject(out Player player);
        
        float curValue = hpSlider.value;   
        float targetValue = player.GetStat().currentHp.Value / 
                            player.GetStat().maxHp.Value;

        if (Math.Abs(curValue - targetValue) > 0.01f)
        {
            hpSlider.value = Mathf.Lerp(curValue, targetValue, 5 * Time.deltaTime);
        }
    }
}
