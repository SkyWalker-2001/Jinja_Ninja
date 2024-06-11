using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue_UI : MonoBehaviour
{
    [SerializeField] private GameObject continue_Button;
    [SerializeField] private VolumeController_UI[] volumeController_UI;

    private void Start() {
        bool showButton = PlayerPrefs.GetInt("Level" + 2 + "Unclocked") == 1;
        continue_Button.SetActive(showButton);

        AudioManager.instance.PlayBGM(0);

        for (int i = 0; i < volumeController_UI.Length; i++)
        {
            volumeController_UI[i].GetComponent<VolumeController_UI>().SetupVolume();
        }
    }

    public void SwitchMenueTo(GameObject uiMenue){
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
            AudioManager.instance.PlaySFX(4);
            uiMenue.SetActive(true);
    }

    public void SetGameDifficulty(int i) => GameManager.instance.difficulty = i;
}
