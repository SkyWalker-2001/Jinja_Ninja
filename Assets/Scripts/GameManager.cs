using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int difficulty;

    [Header("Timer")]
    public float timer;
    public bool startTimer;

    [Header("Level Info")]
    public int levelNumber;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        int gameDifficulty = PlayerPrefs.GetInt("GameDifficulty");
        // value get reset 

        if (difficulty == 0)
        {
            difficulty = gameDifficulty;
        }

        Debug.Log(PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime", timer));

    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }
    }

    public void SaveBestTimer()
    {
        startTimer = false;

        float lastTime = PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime");

        if (timer < lastTime)
            PlayerPrefs.SetFloat("Level" + levelNumber + "BestTime", timer);

        timer = 0;
    }

    public void SaveGame_Difficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }

    public void saveCollectedFruits()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");

        int newTotalFruits = totalFruits + PlayerManager.instance.fruits;

        PlayerPrefs.SetInt("TotalFruitsCollected", newTotalFruits);
        PlayerPrefs.SetInt("Level" + levelNumber + "FruitsCollected", PlayerManager.instance.fruits);

        PlayerManager.instance.fruits = 0; 
    }

    public void SaveLevelInfo()
    {
        int nextLevel = levelNumber + 1;
        PlayerPrefs.SetInt("Level" + nextLevel + "Unlocked", 1);
    }
}
