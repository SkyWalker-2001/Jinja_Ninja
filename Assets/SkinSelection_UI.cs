using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;

    [SerializeField] private Animator anim;
    [SerializeField] private int skin_ID;

    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] priceForSkin;

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

    private void SetupSkinInfo()
    {
        equipButton.SetActive(skinPurchased[skin_ID]);
        buyButton.SetActive(!skinPurchased[skin_ID]);

        if(!skinPurchased[skin_ID])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: "+ priceForSkin[skin_ID];

        anim.SetInteger("skin_id", skin_ID);
    }

    public void Buy()
    {
        skinPurchased[skin_ID] = true;

        SetupSkinInfo();
    }

    public void Equip()
    {
        Debug.Log("Skin was equiped");
    }
}
