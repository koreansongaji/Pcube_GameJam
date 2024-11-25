using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpReader : MonoBehaviour
{
    public Slider expSlider;
    
    private void Update()
    {
        if (GameManager.Pause) return;
        GameManager.Instance.TryGetPlayerObject(out Player player);
        if (player.TryGetComponent(out PlayerLevel playerLevel) is false)
        {
            return;
        }
        
        float curValue = expSlider.value;   
        float targetValue = playerLevel.GetExp() / playerLevel.GetExpToNextLevel();

        if (Mathf.Abs(curValue - targetValue) > 0.01f)
        {
            expSlider.value = Mathf.Lerp(curValue, targetValue, 5 * Time.unscaledDeltaTime);
        }
    }
}
