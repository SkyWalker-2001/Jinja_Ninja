using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private int skin_ID;
    [SerializeField] private int[] priceForSkin;

    [SerializeField] private bool[] skinPurchased;

    [Header("Componenets")]
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;



    public void Next_Skin()
    {
        AudioManager.instance.PlaySFX(4);

        skin_ID++;

        if (skin_ID > 3)
            skin_ID = 0;

        SetupSkinInfo();
    }

    public void Previous_Skin()
    {
        AudioManager.instance.PlaySFX(4);

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

    private bool EnoughMoney()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");

        if (totalFruits > priceForSkin[skin_ID])
        {
            totalFruits = totalFruits - priceForSkin[skin_ID];

            PlayerPrefs.SetInt("TotalFruitsCollected", totalFruits);

            return true;
        }
        AudioManager.instance.PlaySFX(6);
        return false;

    }

    private void SetupSkinInfo()
    {
        skinPurchased[0] = true;

        for (int i = 0; i < skinPurchased.Length; i++)
        {
            bool skinUnlocked = PlayerPrefs.GetInt("SkinPurchase" + i) == 1;

            if (skinUnlocked)
            {
                skinPurchased[i] = true;
            }
        }

        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();

        selectButton.SetActive(skinPurchased[skin_ID]);
        buyButton.SetActive(!skinPurchased[skin_ID]);

        if (!skinPurchased[skin_ID])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skin_ID];

        anim.SetInteger("skin_id", skin_ID);
    }

    public void Buy()
    {
        if (EnoughMoney())
        {
            PlayerPrefs.SetInt("SkinPurchase" + skin_ID, 1);
            SetupSkinInfo();
        }
        else
            Debug.Log("Not Enough Money");
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
