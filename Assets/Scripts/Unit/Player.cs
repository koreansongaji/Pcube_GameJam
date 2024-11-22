using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData data;

    int NowLevel = 1;
    float NeedExpForNextLevel = 30;
    float NowExp = 0;
    float Attack1CoolTime;
    float Attack2CoolTime;
    float HpMax;
    float HpNow;

    UIManager UIManager;

    public void GetExp(int a)
    {
        NowExp += a;
        CheckSufficientExp();
    }

    void CheckSufficientExp()
    {
        if (NowExp >= NeedExpForNextLevel)
        {
            LevelUp();
            
            
            CheckSufficientExp();
        } else
        {
            return;
        }
    }

    public void LevelUp()
    {
        NowLevel++; //레벨업
        NowExp -= NeedExpForNextLevel;
        NeedExpForNextLevel = (NeedExpForNextLevel * 1.3f); //임의로 설정함: 각 레벨단계 사이에서 필요 경험치량이 1.3배 늘어남
        //시간을 멈춘 뒤
        //특성 화면을 연다

    }

    public void Update()
    {
        
    }

}
