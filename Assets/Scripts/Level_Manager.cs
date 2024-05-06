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

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i])
            {
                return;
            }

            string sceneName = "Level " + i;

            GameObject newButton = Instantiate(levelButton, levelButton_parent);
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadSceneMode(sceneName));
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
        }
    }

    public void LoadSceneMode(String sceneToLoad)
    {
        GameManager.instance.SaveGame_Difficulty();
        SceneManager.LoadScene(sceneToLoad);
    }
}
