using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Fruit_Types
{
    Apple,
    Banana,
    Orange,
    Kiwi,
    Pineapple,
    Cherry,
    Melon,
    Strayberry,
}

public class Fruit_Item : MonoBehaviour
{
    [SerializeField]protected Animator anim;
    [SerializeField]protected SpriteRenderer sr;
    public Fruit_Types myFruit_Type;
    [SerializeField] private Sprite[] fruit_Image;
    public Fruit_Types fruitTypes;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            PlayerManager.instance.fruits++;
            Destroy(this.gameObject); 

            // gamemanager.fruits += player.fruits;
        }
    }

    public void FruitSetup(int fruitIndex)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(fruitIndex, 1);
    }

    // private void OnValidate()
    // {
    //     sr.sprite = fruit_Image[((int)myFruit_Type)];
    // }
}
