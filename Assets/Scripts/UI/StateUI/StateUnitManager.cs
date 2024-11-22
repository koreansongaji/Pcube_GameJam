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
    [SerializeField] TextMeshProUGUI RemainSPText;
    [SerializeField] GameObject Description;

    public int remainSP = 11;

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

    public void StateUnitMouseDown(StateUnit.ESkillType skillType, int number)
    {
        if (remainSP>0)
        {
            if (skillType == StateUnit.ESkillType.skill1)
            {
                if (CheckIsRightAccess(skill1, number))
                {
                    if (skill1.transform.GetChild(number).GetComponent<StateUnit>().NowCount < skill1.transform.GetChild(number).GetComponent<StateUnit>().MaxCount)
                    {
                        //이 스텟 활성화
                        skill1.transform.GetChild(number).GetComponent<StateUnit>().NowCount++;
                        skill1.transform.GetChild(number).GetComponent<StateUnit>().StateUnitUpdate();
                        remainSP--;
                        RemainSPText.text = "남은 스탯포인트 : " + remainSP;
                    }
                }

            }
            else if (skillType == StateUnit.ESkillType.skill2)
            {
                if (CheckIsRightAccess(skill2, number))
                {
                    if (skill2.transform.GetChild(number).GetComponent<StateUnit>().NowCount < skill2.transform.GetChild(number).GetComponent<StateUnit>().MaxCount)
                    {
                        //이 스텟 활성화
                        skill2.transform.GetChild(number).GetComponent<StateUnit>().NowCount++;
                        skill2.transform.GetChild(number).GetComponent<StateUnit>().StateUnitUpdate();
                        remainSP--;
                        RemainSPText.text = "남은 스탯포인트 : " + remainSP;
                    }
                }

            }
            else if(skillType == StateUnit.ESkillType.skill3)
            {
                if (CheckIsRightAccess(skill3, number))
                {
                    if (skill3.transform.GetChild(number).GetComponent<StateUnit>().NowCount < skill3.transform.GetChild(number).GetComponent<StateUnit>().MaxCount)
                    {
                        //이 스텟 활성화
                        skill3.transform.GetChild(number).GetComponent<StateUnit>().NowCount++;
                        skill3.transform.GetChild(number).GetComponent<StateUnit>().StateUnitUpdate();
                        remainSP--;
                        RemainSPText.text = "남은 스탯포인트 : " + remainSP;
                    }
                }

            }
        }
        
    }

    bool CheckIsRightAccess(GameObject gameObject, int number)
    {
        int sum = 0;
        for (int i = 0; i < number / 2 * 2+2; i++) //자기 둘까지 포함해서
        {
            sum += gameObject.transform.GetChild(i).GetComponent<StateUnit>().NowCount;
        }
        Debug.Log(sum);
        if(number < 2)
        {
            return sum < 5;
        } else if(number < 4)
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

    public static void Init()
    {
        instance = GameObject.Find("StateUI").GetComponent<StateUnitManager>();

        for(int i=0; i<8; i++)
        {
            if(i%4 > 1)
            {
                instance.skill1.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill1, i, 0, 1);
                //2, 3
            } else
            {
                instance.skill1.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill1, i, 0, 5);
                //0, 1
            }

            instance.skill1.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

        for (int i = 0; i < 8; i++)
        {
            if (i % 4 > 1)
            {
                instance.skill2.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill2, i, 0, 1);
                //2, 3
            }
            else
            {
                instance.skill2.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill2, i, 0, 5);
                //0, 1
            }

            instance.skill2.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }

        for (int i = 0; i < 8; i++)
        {
            if (i % 4 > 1)
            {
                instance.skill3.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill3, i, 0, 1);
                //2, 3
            }
            else
            {
                instance.skill3.transform.GetChild(i).GetComponent<StateUnit>().SetStateUnit(StateUnit.ESkillType.skill3, i, 0, 5);
                //0, 1
            }

            instance.skill3.transform.GetChild(i).GetComponent<StateUnit>().StateUnitUpdate();
        }
    }

    public void MarkDescription(Sprite sprite, string context)
    {
        Description.SetActive(true);
        Description.transform.Find("Img").GetComponent<Image>().sprite = sprite;
        Description.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = context;
    }
    public void ExitDescription()
    {
        Description.SetActive(false);
    }
}
