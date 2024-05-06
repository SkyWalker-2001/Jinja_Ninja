using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentFruitAmount;


    private void Update()
    {
        timerText.text = "Timer " + GameManager.instance.timer.ToString("00") + " s";

        currentFruitAmount.text = PlayerManager.instance.fruits.ToString();
    }
}
