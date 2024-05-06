using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue_UI : MonoBehaviour
{
    public void SwitchMenueTo(GameObject uiMenue){
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
            uiMenue.SetActive(true);
    }

    public void SetGameDifficulty(int i) => GameManager.instance.difficulty = i;
}
