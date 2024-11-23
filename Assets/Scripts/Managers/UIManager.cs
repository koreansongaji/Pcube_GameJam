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
    public Player player;
    
    public ESeason NowSeason;
    [SerializeField] Color summerColor;
    [SerializeField] Color winterColor;
    [SerializeField] Color springColor;
    [SerializeField] Color fallColor;

    [SerializeField] GameObject GameStopObject;
    public GameObject StateUI;
    bool isGameStopOpened = false;

    private void Awake()
    {
        OpenStateToggle();
        StateUnitManager.Init(this);
    }

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
    }

    public void OpenStateToggle()
    {
        StateUnitManager.Instance.gameObject.SetActive(true);
        StateUnitManager.Instance.RenewalStateUI(player.skill1, player.skill2, player.skill3, player.statePoint);
    }

    public void CloseStateToggle()
    {
        StateUnitManager.Instance.gameObject.SetActive(false);
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
            case (ESeason.WINTER):
                SeasonSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = winterColor;
                break;
            case (ESeason.SUMMER):
                SeasonSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = summerColor;
                break;
            case (ESeason.SPRING):
                SeasonSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = fallColor;
                break;
            case (ESeason.FALL):
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
    
    public enum ESeason
    {
        SUMMER,
        WINTER,
        SPRING,
        FALL
    }
}
