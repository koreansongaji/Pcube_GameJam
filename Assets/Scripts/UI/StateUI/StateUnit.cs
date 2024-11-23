using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StateUnit : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    int number; //이 스텟유닛이 어디에 있는지
    public int NowCount;
    public int MaxCount; // 1 또는 5
    Material grayMaterial;
    [SerializeField] string context;

    bool isGameStopObject;

    public enum ESkillType
    {
        skill1, skill2, skill3
    }
    ESkillType skillType;

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void SetStateUnit(ESkillType skillType, int a, int NowCount, int MaxCount, bool isGameStopObject)
    {
        this.skillType = skillType;
        this.number = a;
        this.NowCount = NowCount;
        this.MaxCount = MaxCount;
        this.isGameStopObject = isGameStopObject;
        grayMaterial = new Material(GetComponent<Image>().material);
        GetComponent<Image>().material = grayMaterial;
    }
    public void StateUnitUpdate()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = NowCount + "/" + MaxCount;
        if(NowCount == 0)
        {
            GetComponent<Image>().material.SetFloat("_Grayscale", 1);
        } else
        {
            GetComponent<Image>().material.SetFloat("_Grayscale", 0);
        }

    }
}
