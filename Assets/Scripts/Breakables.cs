using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class Breakables : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectpieces;

    public bool shouldDropItems = false;

    [Header(("droppable items"))]
    [SerializeField]
    GameObject[] itemsToDrop;

    [SerializeField]
    [Range(0,100)]
    private float itemDropPercentage;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            #region shattered pieces 
            //Instantiate(deathSplatter[selectedSplat], transform.position, transform.rotation);
            for (int i = 0; i < objectpieces.Length; i++)
            {
                int rotation = UnityEngine.Random.Range(0, 4);
                Instantiate(objectpieces[i], transform.position, Quaternion.Euler(0, 0, rotation * 90f));
            }
           
            #endregion
            if (shouldDropItems)
            {
            #region drop items

                // box will drop an item , we will start with some potions
                float dropChance = UnityEngine.Random.Range(1, 100);
                if (dropChance < itemDropPercentage)
                {
                    int itemIndex = UnityEngine.Random.Range(0, itemsToDrop.Length);
                    Instantiate(itemsToDrop[itemIndex] , transform.position , transform.rotation);
                }
            #endregion 
            }

            Destroy(gameObject);
        }
    
    }

    /// <summary>
    /// This function is to determine the chance of an item dropping from the box
    /// </summary>
    private void Drop()
    {
        Random generator = new Random();
        const int probabilityWindow = 30;
        int randomChance = generator.Next(0, 100);

        if (randomChance < probabilityWindow)
        {
            // spawn
        }
    }
}
