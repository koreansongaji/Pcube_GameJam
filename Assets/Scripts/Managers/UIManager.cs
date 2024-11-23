using Helpers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Generic.Singleton<UIManager>
{
    // Start is called before the first frame update
    [SerializeField] Slider HPBar;
    [SerializeField] Slider ExpBar;
    [SerializeField] Slider SeasonSlider;
    public static UIManager instance;
    public Player player;

    public enum ESeason
    {
        summer,
        winter,
        spring,
        fall
    }
    public ESeason NowSeason;
    [SerializeField] Color summerColor;
    [SerializeField] Color winterColor;
    [SerializeField] Color springColor;
    [SerializeField] Color fallColor;

    [SerializeField] GameObject GameStopObject;
    public GameObject StateUI;
    bool isGameStopOpened = false;
    bool isStateUIWindowOpened = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameStopOpened)
            {
                isGameStopOpened = false;
                Time.timeScale = 1;
                GameStopObject.SetActive(false);

            }
            else
            {
                Time.timeScale = 0;
                isGameStopOpened = true;
                GameStopObject.SetActive(true);
                StateUnitManager.Instance.RenewalGameStopStateUI(player.skill1, player.skill2, player.skill3);
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && !isGameStopOpened)
        {
            SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_ANSWER_01);
            if (isStateUIWindowOpened)
            {
                CloseStateToggle();
                isStateUIWindowOpened = false;
            }
            else
            {
                OpenStateToggle();
                isStateUIWindowOpened = true;
            }
        }


        if(!isGameStopOpened && !isStateUIWindowOpened)
        {
            for(int i=0; i<GameManager.Instance.ExpPoolTransformSub.transform.childCount; i++)
            {
                if ((GameManager.Instance.ExpPoolTransformSub.GetChild(i).transform.position -
                     MouseCursorPosFinder.GetMouseWorldPosition()).magnitude < 1f) {
                    GameManager.Instance.ExpPoolTransformSub.GetChild(i).GetComponent<ExpSphere>().ActiveThisExpSphere();
                } 
            }
        }
    }


    public void OnDestroy()
    {
        instance = null;
    }

    public void OpenStateToggle()
    {
        StateUnitManager.Instance.gameObject.SetActive(true);
        StateUnitManager.Instance.RenewalStateUI(player.skill1, player.skill2, player.skill3, player.statePoint);
    }

    public void CloseStateToggle()
    {
        StateUnitManager.Instance.gameObject.SetActive(false);
        StateUnitManager.Instance.Description.SetActive(false);
    }
    public void SetHPBar(float a)
    {
        HPBar.value = a;
    }
    public void SetExpBar(float a)
    {
        ExpBar.value = a;
    }

    public void SetSeasonBar(float a)
    {
        float sliderValue = a / 8.0f;
    }

    public void SeasonChange(ESeason season)
    {
        this.NowSeason = season;

        switch (season)
        {
            case (ESeason.winter):
                SeasonSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = winterColor;
                break;
            case (ESeason.summer):
                SeasonSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = summerColor;
                break;
            case (ESeason.spring):
                SeasonSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = fallColor;
                break;
            case (ESeason.fall):
                SeasonSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = fallColor;
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameContinue()
    {
        isGameStopOpened = false;
        Time.timeScale = 1;
        GameStopObject.SetActive(false);
    }
}
