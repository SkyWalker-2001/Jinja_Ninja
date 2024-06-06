using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButton_parent;

    [SerializeField] private bool[] levelOpen;

    private void Start()
    {
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);

        AssignLevelBoolean();

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i])
            {
                return;
            }

            string sceneName = "Level " + i;

            GameObject newButton = Instantiate(levelButton, levelButton_parent);
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadSceneMode(sceneName));
            newButton.GetComponent<LevelInfo_Button>().UpdateLevelSelection_UI(levelNum: i);
        }
    }

    private void AssignLevelBoolean()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (unlocked)
            {
                levelOpen[i] = true;
            }
            else
            {
                return;
            }

        }
    }

    public void LoadSceneMode(String sceneToLoad)
    {
        AudioManager.instance.PlaySFX(4);
        GameManager.instance.SaveGame_Difficulty();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadNewGame()
    {

        // 0 is mainMenue
        // 1 is default level always available
        AudioManager.instance.PlaySFX(4);

        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (unlocked)
                PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
            else
                SceneManager.LoadScene("Level 1");
            return;
        }
    }

    public void LoadToContinue()
    {
        AudioManager.instance.PlaySFX(4);

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (!unlocked)
            {
                SceneManager.LoadScene("Level " + (i - 1));
                return;
            }

        }
    }
}
