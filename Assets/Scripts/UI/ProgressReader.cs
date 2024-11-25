using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProgressReader : MonoBehaviour
{
    [FormerlySerializedAs("expSlider")] public Slider progressSlider;
    
    private void Update()
    {
        if (GameManager.Pause) return;

        float curValue = progressSlider.value;
        float targetValue = GameManager.Instance.GetProgress();

        if (Mathf.Abs(curValue - targetValue) > 0.01f)
        {
            progressSlider.value = Mathf.Lerp(curValue, targetValue, 5 * Time.unscaledDeltaTime);
        }
    }
}
