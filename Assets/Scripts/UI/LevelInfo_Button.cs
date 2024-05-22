using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfo_Button : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName_TMPro;
    [SerializeField] private TextMeshProUGUI bestTime_TMPro;
    [SerializeField] private TextMeshProUGUI totalFruits_TMPro;
    [SerializeField] private TextMeshProUGUI collectedFruits_TMPro;

    public void UpdateLevelSelection_UI(int levelNum)
    {
        levelName_TMPro.text = "Level " + levelNum;
        bestTime_TMPro.text = "Best Time: " + PlayerPrefs.GetFloat("Level" + levelNum + "BestTime",999).ToString("00") + " sec";
        collectedFruits_TMPro.text = PlayerPrefs.GetInt("Level" + levelNum + "FruitsCollected", PlayerManager.instance.fruits).ToString() ;
        totalFruits_TMPro.text = "/ " + PlayerPrefs.GetInt("TotalFruitsCollected");
    }
}
