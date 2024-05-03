using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButton_parent;

    private void Start()
    {

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = "Level " + i;

            GameObject newButton = Instantiate(levelButton, levelButton_parent);
            newButton.AddComponent<Button>().onClick.AddListener(() => LoadSceneMode(sceneName));
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
        }
    }

    public void LoadSceneMode(String sceneToLoad) => SceneManager.LoadScene(sceneToLoad);
    
}
