using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSphere : MonoBehaviour
{
    float a = 1f;
    bool isActive = false;


    public void ActiveThisExpSphere()
    {
        if (!isActive)
        {
            isActive = true;
            transform.DOMove(GameManager.Instance.player.transform.position + new Vector3(0, 2, 0), 0.5f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                GameManager.Instance.player.statePoint++;
                SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_APPEAR_01);
                gameObject.transform.parent = GameManager.Instance.ExpPoolTransform;
                GameManager.Instance._expPool.Release(gameObject);
            });
        }

    }

    public void OnEnable()
    {
        isActive = false;
    }
}
