using DG.Tweening;
using TMPro;
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

    
    public ESeason NowSeason;
    [SerializeField] Color summerColor;
    [SerializeField] Color winterColor;
    [SerializeField] Color springColor;
    [SerializeField] Color fallColor;

    [SerializeField] GameObject GameStopObject;
    public GameObject StateUI;
    bool isGameStopOpened = false;
    bool isStateUIWindowOpened = false;

    protected override void Awake()
    {
    }

    public void DamageFloat(Vector2 vec, float damage)
    {
        GameObject go = new GameObject();
        go.transform.parent = GameObject.Find("Canvas").transform;
        go.transform.position = vec;

        TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
        text.text = (int)damage + "";
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = 30;
        text.DOFade(0.5f, 0.5f).OnComplete(() =>
        {
            Destroy(go);
        });
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
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && !isGameStopOpened)
        {
            SoundManager.Instance.PlaySE(SoundManager.ESoundEffect.SE_ANSWER_01);
            if (isStateUIWindowOpened)
            {
                isStateUIWindowOpened = false;
            }
            else
            {
                isStateUIWindowOpened = true;
            }
        }
    }


    public void OnDestroy()
    {
        instance = null;
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
}
