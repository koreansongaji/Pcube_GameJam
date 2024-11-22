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
        NowLevel++; //������
        NowExp -= NeedExpForNextLevel;
        NeedExpForNextLevel = (NeedExpForNextLevel * 1.3f); //���Ƿ� ������: �� �����ܰ� ���̿��� �ʿ� ����ġ���� 1.3�� �þ
        //�ð��� ���� ��
        //Ư�� ȭ���� ����

    }

    public void Update()
    {
        
    }

}
