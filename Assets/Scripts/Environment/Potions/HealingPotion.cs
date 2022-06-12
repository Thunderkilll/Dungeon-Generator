using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    #region props
    [Header("Healing props")]
    [SerializeField]
    float healAmount = 1f;


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player")
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
}
