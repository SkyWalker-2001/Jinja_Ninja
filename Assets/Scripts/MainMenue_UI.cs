using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue_UI : MonoBehaviour
{
    [SerializeField] private GameObject continue_Button;
    private void Start() {
        bool showButton = PlayerPrefs.GetInt("Level" + 2 + "Unclocked") == 1;
        continue_Button.SetActive(showButton);
    }

    public void SwitchMenueTo(GameObject uiMenue){
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
            uiMenue.SetActive(true);
    }

    public void SetGameDifficulty(int i) => GameManager.instance.difficulty = i;
}
