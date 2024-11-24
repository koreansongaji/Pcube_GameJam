using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerScripts;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private Button[] upgradeButtons;
        private PlayerLevel playerLevel;
        
        [SerializeField] private StatUpgrader statUpgrader;
        [SerializeField] private List<UpgradeStat> currentUpgradeStats;

        private void Start()
        {
            if (GameManager.Instance.TryGetPlayerObject(out Player player))
            {
                playerLevel = player.GetComponent<PlayerLevel>();
                playerLevel.OnLevelUp.AddListener(OnPlayerLevelUp);
            }

            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                int index = i;
                upgradeButtons[i].onClick.AddListener(() => OnUpgradeButtonClicked(index));
            }

            upgradePanel.SetActive(false);
            ReloadUpgradeButtons();
        }


        private int remainingUpgradeCount = 0;
        private void OnPlayerLevelUp(int levelUpCount)
        {
            Debug.Log($"Player level up! Level : {playerLevel.GetLevel()}");
            
            remainingUpgradeCount += levelUpCount;
            GameManager.Pause = true;
            upgradePanel.SetActive(true);
        }

        private void OnUpgradeButtonClicked(int index)
        {
            Debug.Log($"Upgrade option {index} selected");
            
            statUpgrader.ApplySelectedUpgradeStat(currentUpgradeStats[index]);
            remainingUpgradeCount--;
            
            
            if (remainingUpgradeCount <= 0)
            {
                GameManager.Pause = false;
                upgradePanel.SetActive(false);
            }
            else
            {
                ReloadUpgradeButtons();
            }
        }
        
        private void ReloadUpgradeButtons()
        {
            currentUpgradeStats = statUpgrader.GetRandomUpgradeStats();
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                upgradeButtons[i].GetComponentInChildren<Text>().text = 
                    currentUpgradeStats[i].statType.ToString() + " : " + currentUpgradeStats[i].value;
            }
        }
    }
}