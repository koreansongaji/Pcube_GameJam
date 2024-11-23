using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ExpSphere : MonoBehaviour
{
    bool isActive = false;

    public void ActiveThisExpSphere()
    {
        if (isActive) return;
        
        isActive = true;
        GameManager.Instance.TryGetPlayerObject(out Player player);
            
        transform.DOMove(player.transform.position + new Vector3(0, 2, 0), 0.5f)
            .SetEase(Ease.InExpo).OnComplete(() =>
            {
                player.statePoint++;
                SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_APPEAR_01);
                ExpPoolSystem.Instance.ReturnExpSphere(gameObject);
            });
    }

    private void Update()
    {
        if (isActive is false) return;
        if (GameManager.Pause) return;
        
        Vector3 getMousePos = Helpers.MouseCursorPosFinder.GetMouseWorldPosition();
    }

    public void OnEnable()
    {
        isActive = false;
    }
}
