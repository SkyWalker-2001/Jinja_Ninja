using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Transform[] fruitPosition;
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private bool randomFruits;

    private int fruitIndex;

    private void Start()
    {
        fruitPosition = GetComponentsInChildren<Transform>();
        int levelNum = GameManager.instance.levelNumber;

        for (int i = 1; i < fruitPosition.Length; i++)
        {
            GameObject newFruit = Instantiate(fruitPrefab, fruitPosition[i]);

            if (randomFruits)
            {
                fruitIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Fruit_Types)).Length);
                newFruit.GetComponent<Fruit_Item>().FruitSetup(fruitIndex);
            }
            else
            {
                newFruit.GetComponent<Fruit_Item>().FruitSetup(fruitIndex);
                fruitIndex++;

                if (fruitIndex > Enum.GetNames(typeof(Fruit_Types)).Length)
                {
                    fruitIndex = 0;
                }
            }

            fruitPosition[i].GetComponent<SpriteRenderer>().sprite = null;
        }
        
        int totalAmountOfFruits = PlayerPrefs.GetInt("Level" + levelNum + "TotalFruits" );

        if(totalAmountOfFruits != fruitPosition.Length - 1)
            PlayerPrefs.SetInt("Level" + levelNum + "TotalFruits", fruitPosition.Length - 1);
    }
}
