using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StateUnitManager : MonoBehaviour
{

    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
    [SerializeField] GameObject skill3;

    [SerializeField] GameObject skill1StopScene;
    [SerializeField] GameObject skill2StopScene;
    [SerializeField] GameObject skill3StopScene;

    [SerializeField] Text RemainSPText;
    public GameObject Description;
    [SerializeField] GameObject DescriptionStopScene;
    [SerializeField] Button ResetButton;
    [SerializeField] Button ConfirmButton;

    public int remainSP = 11;
    UIManager UIObject;
    private static StateUnitManager instance;
    public static StateUnitManager Instance
    {
        get
        {
            if (!instance)
            {
                Debug.LogError("오류! 스텟 메니저의 Init 함수를 실행하지 않았음!");
            }
            return instance;
        }
    }
    bool isChanged = false;
    public void StateUnitMouseDown(StateUnit.ESkillType skillType, int number)
    {
        if (remainSP > 0)
        {
            if (skillType == StateUnit.ESkillType.skill1)
            {
                if (CheckIsRightAccess(skill1, number))
                {
                    SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_APPEAR_01);
                    if (skill1.transform.GetChild(number).GetComponent<StateUnit>().NowCount < skill1.transform.GetChild(number).GetComponent<StateUnit>().MaxCount)
                    {
                        //이 스텟 활성화
                        skill1.transform.GetChild(number).GetComponent<StateUnit>().NowCount++;
                        skill1.transform.GetChild(number).GetComponent<StateUnit>().StateUnitUpdate();
                        remainSP--;
                        isChanged = true;
                        RemainSPText.text = "남은 스탯포인트 : " + remainSP;
                    }
                    else
                    {
                        SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_BEEP_01);
                    }
                }

            }
            else if (skillType == StateUnit.ESkillType.skill2)
            {
                if (CheckIsRightAccess(skill2, number))
                {
                    SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_APPEAR_01);
                    if (skill2.transform.GetChild(number).GetComponent<StateUnit>().NowCount < skill2.transform.GetChild(number).GetComponent<StateUnit>().MaxCount)
                    {
                        //이 스텟 활성화
                        skill2.transform.GetChild(number).GetComponent<StateUnit>().NowCount++;
                        skill2.transform.GetChild(number).GetComponent<StateUnit>().StateUnitUpdate();
                        remainSP--;
                        isChanged = true;
                        RemainSPText.text = "남은 스탯포인트 : " + remainSP;
                    }
                }
                else
                {
                    SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_BEEP_01);
                }

            }
            else if (skillType == StateUnit.ESkillType.skill3)
            {
                if (CheckIsRightAccess(skill3, number))
                {
                    SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_APPEAR_01);
                    if (skill3.transform.GetChild(number).GetComponent<StateUnit>().NowCount < skill3.transform.GetChild(number).GetComponent<StateUnit>().MaxCount)
                    {
                        //이 스텟 활성화
                        skill3.transform.GetChild(number).GetComponent<StateUnit>().NowCount++;
                        skill3.transform.GetChild(number).GetComponent<StateUnit>().StateUnitUpdate();
                        remainSP--;
                        RemainSPText.text = "남은 스탯포인트 : " + remainSP;
                    }
                }
                else
                {
                    SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_BEEP_01);
                }

            }

        }
        else
        {
            SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_BEEP_01);
        }

    }

    bool CheckIsRightAccess(GameObject gameObject, int number)
    {
        int sum = 0;
        for (int i = 0; i < number / 2 * 2 + 2; i++) //자기 둘까지 포함해서
        {
            sum += gameObject.transform.GetChild(i).GetComponent<StateUnit>().NowCount;
        }
        Debug.Log(sum);
        if (number < 2)
        {
            return sum < 5;
        }
        else if (number < 4)
        {
            return sum < 6 && sum >= 5;
        }
        else if (number < 6)
        {
            return sum < 11 && sum >= 6;
        }
        else
        {
            return sum < 12 && sum >= 11;
        }


    }

    public static void Init(UIManager uIManager) //완전처음
    {

        instance = uIManager.StateUI.GetComponent<StateUnitManager>();
        instance.UIObject = uIManager;
        for (int i = 0; i < 8; i++)
        {
            if (i % 4 > 1)
            {
                instance.skill1.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill1, i, 0, 1, false);
                instance.skill2.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill2, i, 0, 1, false);
                instance.skill3.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill3, i, 0, 1, false);
                //2, 3
            }
            else
            {
                instance.skill1.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill1, i, 0, 5, false);
                instance.skill2.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill2, i, 0, 5, false);
                instance.skill3.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill3, i, 0, 5, false);
                //0, 1
            }

            instance.skill1.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
            instance.skill2.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
            instance.skill3.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }



        for (int i = 0; i < 8; i++)
        {
            if (i % 4 > 1)
            {
                instance.skill1StopScene.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill1, i, 0, 1, true);
                instance.skill2StopScene.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill2, i, 0, 1, true);
                instance.skill3StopScene.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill3, i, 0, 1, true);
                //2, 3
            }
            else
            {
                instance.skill1StopScene.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill1, i, 0, 5, true);
                instance.skill2StopScene.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill2, i, 0, 5, true);
                instance.skill3StopScene.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill3, i, 0, 5, true);
                //0, 1
            }

            instance.skill1StopScene.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
            instance.skill2StopScene.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
            instance.skill3StopScene.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

        instance.gameObject.SetActive(false);
    }

    public void MarkDescription(Sprite sprite, string context)
    {
        Description.SetActive(true);
        Description.transform.Find("Img").GetComponent<Image>().sprite = sprite;
        Description.transform.Find("Text").GetComponent<Text>().text = context;
    }

    public void MarkDescriptionGameStop(Sprite sprite, string context)
    {
        DescriptionStopScene.SetActive(true);
        DescriptionStopScene.transform.Find("Img").GetComponent<Image>().sprite = sprite;
        DescriptionStopScene.transform.Find("Text").GetComponent<Text>().text = context;
    }
    public void ExitDescription()
    {
        Description.SetActive(false);
    }
    public void ExitDescriptionGameStop()
    {
        DescriptionStopScene.SetActive(false);
    }

    public void RenewalStateUI(int[] skill1Arr, int[] skill2Arr, int[] skill3Arr, int SP)
    {
        remainSP = SP;
        RemainSPText.text = "남은 스탯 포인트 : " + remainSP;

        isChanged = false;

        for (int i = 0; i < 8; i++)
        {
            skill1.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill1Arr[i];
            skill1.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

        for (int i = 0; i < 8; i++)
        {
            skill2.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill2Arr[i];
            skill2.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

        for (int i = 0; i < 8; i++)
        {
            skill3.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill3Arr[i];
            skill3.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }
    }
    public void RenewalGameStopStateUI(int[] skill1Arr, int[] skill2Arr, int[] skill3Arr)
    {
        DescriptionStopScene.SetActive(false);
        for (int i = 0; i < 8; i++)
        {
            skill1StopScene.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill1Arr[i];
            skill1StopScene.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();

            skill2StopScene.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill2Arr[i];
            skill2StopScene.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();

            skill3StopScene.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill3Arr[i];
            skill3StopScene.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

    }

    public void GameStop(int[] skill1Arr, int[] skill2Arr, int[] skill3Arr)
    {
        for (int i = 0; i < 8; i++)
        {
            skill1.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill1Arr[i];
            skill1.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

        for (int i = 0; i < 8; i++)
        {
            skill2.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill2Arr[i];
            skill2.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

        for (int i = 0; i < 8; i++)
        {
            skill3.transform.GetChild(i).GetComponent<StateUnit>().NowCount = skill3Arr[i];
            skill3.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }
    }

    public void ConfirmButtonClick()
    {
        for (int i = 0; i < 8; i++)
        {
            UIObject.player.skill1[i] = skill1.transform.GetChild(i).GetComponent<StateUnit>().NowCount;
            UIObject.player.skill2[i] = skill2.transform.GetChild(i).GetComponent<StateUnit>().NowCount;
            UIObject.player.skill3[i] = skill3.transform.GetChild(i).GetComponent<StateUnit>().NowCount;
        }
        SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_BEAT_01);
        UIObject.player.statePoint = remainSP;
        //먼가 넘기기
    }

    public void ResetButtonClick()
    {
        RenewalStateUI(UIObject.player.skill1, UIObject.player.skill2, UIObject.player.skill3, UIObject.player.statePoint);
        SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_ANSWER_01);
    }

    public void CloseStateUIButtonClick()
    {
        UIObject.CloseStateToggle();
    }
}
