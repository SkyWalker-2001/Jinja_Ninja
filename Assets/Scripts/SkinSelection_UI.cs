using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;

    [SerializeField] private Animator anim;
    [SerializeField] private int skin_ID;

    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] priceForSkin;

    [SerializeField] private TextMeshProUGUI bankText;

    public void Next_Skin()
    {
        skin_ID++;

        if (skin_ID > 3)
            skin_ID = 0;

        SetupSkinInfo();
    }

    public void Previous_Skin()
    {
        skin_ID--;

        if (skin_ID < 0)
            skin_ID = 3;

        SetupSkinInfo();
    }

    private void OnEnable()
    {
        SetupSkinInfo();
    }

    private void OnDisable()
    {
        selectButton.SetActive(false);
    }

    private void SetupSkinInfo()
    {
        skinPurchased[0] = true;

        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();

        selectButton.SetActive(skinPurchased[skin_ID]);
        buyButton.SetActive(!skinPurchased[skin_ID]);

        if (!skinPurchased[skin_ID])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skin_ID];

        anim.SetInteger("skin_id", skin_ID);
    }

    public void Buy()
    {
        skinPurchased[skin_ID] = true;

        SetupSkinInfo();
    }

    public void Select()
    {
        PlayerManager.instance.choosenSkinId = skin_ID;
        Debug.Log("Skin was equiped");
    }

    public void SwitchSelectButton(GameObject newButton)
    {
        selectButton = newButton;
    }
}
