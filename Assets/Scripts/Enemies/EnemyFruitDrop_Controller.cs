using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFruitDrop_Controller : MonoBehaviour
{
    [SerializeField] private GameObject enemydrop_fruit_Prefab;
    
    [Range(1,10)]
    [SerializeField] private int dropAmount;

    public void DropFruit(){
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject newFruit = Instantiate(enemydrop_fruit_Prefab, transform.position, transform.rotation);
            Destroy(newFruit, 10);
        }
    }
}
