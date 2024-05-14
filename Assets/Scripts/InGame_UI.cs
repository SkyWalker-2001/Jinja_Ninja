using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGame_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentFruitAmount;

    private void Start() 
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        timerText.text = "Timer " + GameManager.instance.timer.ToString("00") + " s";

        currentFruitAmount.text = PlayerManager.instance.fruits.ToString();
    }
}
