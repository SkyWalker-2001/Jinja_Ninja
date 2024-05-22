using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGame_UI : MonoBehaviour
{

    private bool gamePause;

    [Header("Menue GameObject")]
    [SerializeField] private GameObject inGame_UI;
    [SerializeField] private GameObject pauseMenue_UI;
    [SerializeField] private GameObject endLevel_UI;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentFruitAmount;
    [SerializeField] private TextMeshProUGUI endTime_Text;
    [SerializeField] private TextMeshProUGUI endBestTime_Text;
    [SerializeField] private TextMeshProUGUI endFruit_Text;

    private void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        Switch_UI(inGame_UI);
    }

    private void Update()
    {
        UpdateInGameInfo();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckIfPaused();
        }
    }

    private bool CheckIfPaused()
    {
        if (!gamePause)
        {
            gamePause = true;
            Time.timeScale = 0;
            Switch_UI(pauseMenue_UI);
            return true;
        }
        else
        {
            gamePause = false;
            Time.timeScale = 1;
            Switch_UI(inGame_UI);
            return false;
        }
    }

    public void On_LevelFinish()
    {
        endFruit_Text.text = "Fruits: " + PlayerManager.instance.fruits;
        endTime_Text.text = "Best time: " + GameManager.instance.timer.ToString("00") + " s";
        endBestTime_Text.text = "Best time: " + PlayerPrefs.GetFloat("Level" + GameManager.instance.levelNumber + "BestTime", 999).ToString("00") + " s";

        Switch_UI(endLevel_UI);
    }

    private void UpdateInGameInfo()
    {
        timerText.text = "Timer " + GameManager.instance.timer.ToString("00") + " s";

        currentFruitAmount.text = PlayerManager.instance.fruits.ToString();
    }

    public void Switch_UI(GameObject uiMenue)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenue.SetActive(true);
    }

    public void Load_MainMenue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_Menue");
    }
    public void ReloadCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void Load_NextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
