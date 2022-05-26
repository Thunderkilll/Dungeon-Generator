using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectpieces;

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            #region blood
      
          
            //Instantiate(deathSplatter[selectedSplat], transform.position, transform.rotation);
            for (int i = 0; i < objectpieces.Length; i++)
            {
                int rotation = Random.Range(0, 4);
                Instantiate(objectpieces[i], transform.position, Quaternion.Euler(0, 0, rotation * 90f));
            }
           
            #endregion

            Destroy(gameObject);
        }
    }
}
