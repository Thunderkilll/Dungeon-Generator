using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    #region props
    [Header("Healing props")]
    [SerializeField]
    float healAmount = 1f;

    public float waitToBeCollected = .5f;

    [Header("Item properties")] 
    public Item item;
    #endregion
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player" && waitToBeCollected <= 0)
        {
            float maxHealth  = PlayerSurvival.instance.GetMaxHealth();
            float currHealth = PlayerSurvival.instance.GetHealth();
            if (currHealth != maxHealth)
            {
                float heal = currHealth + healAmount;
                if (heal > maxHealth)
                {
                    PlayerSurvival.instance.SetHealth(maxHealth);
                }
                else
                {
                    PlayerSurvival.instance.SetHealth(heal);
                }
                Debug.Log("you are drinking a potion amount <color=green>"+healAmount+"</color>");
                Destroy(gameObject);
            }
            else
            {
                //you can pick this up 
                // your health is full
                Debug.Log("Health is <color=red> 100% </color>");
            }
            
        }
    }


    void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }
}
