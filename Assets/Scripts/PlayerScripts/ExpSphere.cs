using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ExpSphere : MonoBehaviour
{
    bool _isActive = false;
    
    

    private void ActiveThisExpSphere()
    {
        if (_isActive) return;
        
        _isActive = true;
        GameManager.Instance.TryGetPlayerObject(out Player player);

        transform.DOMove(player.transform.position + new Vector3(0, 2, 0), 0.5f)
            .SetEase(Ease.InExpo).OnComplete(() =>
            {
            player.GetComponent<PlayerLevel>().EarnExp(10);
            SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_APPEAR_01);
            transform.DOMove(player.transform.position, 0.5f).OnComplete(() => ExpPoolSystem.Instance.ReturnExpSphere(gameObject));
            });
    }

    private void Update()
    {
        if (GameManager.Pause) return;
        
        Vector3 getMousePos = Helpers.MouseCursorPosFinder.GetMouseWorldPosition();
        if (Vector3.Distance(getMousePos, transform.position) < 2f)
        {
            ActiveThisExpSphere();
        }
    }

    public void OnEnable()
    {
        _isActive = false;
    }

    private void OnDrawGizmosSelected()
    {
        // draw mouse pos
        Vector3 getMousePos = Helpers.MouseCursorPosFinder.GetMouseWorldPosition();
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(getMousePos, 0.5f);
    }
}
