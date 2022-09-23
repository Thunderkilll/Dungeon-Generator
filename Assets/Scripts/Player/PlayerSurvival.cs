using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurvival : MonoBehaviour
{
    #region static var
    public static PlayerSurvival instance;
    #endregion
    #region Global Var
    [Header("Player Health")]
    public float health = 100f; //change with damage amount or hunger or thirst
    public float maxHealth = 100f;

    [Header("Player Hunger")]
    public float hunger = 0f; //starting hunger val , current hunger 
    public float maxHunger = 100f; // max hunger a player can survive
    public float hungerFactor = .02f;
    public float hungerDamage = 1f;

    [Header("Player Thirst")]
    public float thirst = 0f; //starting thirst val , current thirst meter 
    public float maxThirst = 100f; // max thirst a player can survive
    public float thirstFactor = .02f;
    public float thirstDamage = 1f;

    [Header("Player Stamina")]
    public float stamina;
    public float maxStamina = 100f;
    public float staminaFactor = 1f;
    float currpeed;

    [Header("Player Damaged status")]
    public float damageInvincLength = 1f;
    private float invincCounter;
    bool isDead = false;

    public GameObject[] deathSplatter;
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; //if we are starting game first time; I should save this value to an internal db
        stamina = maxStamina;
        currpeed = PlayerController.instance.GetMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        // HungerMeterCalculations();
        // ThirstMeterCalculations();
        StaminaMeterCalculations();
        CheckPlayerHealth();
        InvincibilityWhenHit();
        //hungerslider.value = hunger / maxHunger;
        //thirstslider.value = thirst / maxThirst; 

    }

    private void InvincibilityWhenHit()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
            if (invincCounter <= 0)
            {
                PlayerController.instance.ChangeAlphaSprite(1f);
            }
        }
    }

    private void StaminaMeterCalculations()
    {
        //we test if player is moving 
        if (PlayerController.instance.GetMoveInput() != Vector2.zero)
        {
            if (stamina > 0f)
            {
                stamina -= staminaFactor * Time.deltaTime;
            }
            else
            {
                PlayerController.instance.ChangeSpeed(0f);
            }

        }
        //if player is resting 
        if (PlayerController.instance.GetMoveInput() == Vector2.zero)
        {
            if (stamina < maxStamina)
            {
                stamina += staminaFactor * Time.deltaTime;
                PlayerController.instance.ChangeSpeed(currpeed);
            }
        }
        if (stamina >= maxStamina)
        {

            PlayerController.instance.ChangeSpeed(currpeed);

        }
    }

    private void HungerMeterCalculations()
    {
        if (hunger < maxHunger)
        {
            hunger += hungerFactor * Time.deltaTime;
            //Debug.Log("hunger factor : <color=red>" + hunger + "</color>");
        }
        if (hunger >= maxHunger && health > 0)
        {
            hunger = maxHunger;
            health -= hungerDamage * Time.deltaTime; //take damage when hungry
            //Debug.Log("you are hungry , you'll die from hunger find food source now");
        }

    }
    private void ThirstMeterCalculations()
    {
        if (thirst < maxThirst)
        {
            thirst += thirstFactor * Time.deltaTime;
            //Debug.Log("thirst factor : <color=blue>" + thirst+"</color>");

        }
        if (thirst >= maxThirst && health > 0)
        {
            thirst = maxThirst;
            health -= thirstDamage * Time.deltaTime; //take damage when hungry
            //Debug.Log("you are thirsty , you'll die from hydration find a water source now");
        }


    }

    public void TakeDamage(float value)
    {
        //if we take a hit we got invincible for some time
        if (invincCounter <= 0)
        {
            
           
            if (value <= 5)
            {
                //sfx LIGHT HIT
                AudioManager.instance.PlaySFX(30);
                //sfx Heavy Hit
                AudioManager.instance.PlaySFX(33);
            }
            else if (value <= 10 && value > 5)
            {
                //sfx Heavy Hit
                AudioManager.instance.PlaySFX(31);
                //sfx Heavy Hit
                AudioManager.instance.PlaySFX(34);
            }
            else if (value <= 20 && value > 10)
            {
                //sfx Heavy Hit
                AudioManager.instance.PlaySFX(32);
                //sfx Heavy Hit
                AudioManager.instance.PlaySFX(33);
            }
            else  
            {
               
                //sfx Heavy Hit
                AudioManager.instance.PlaySFX(34);
            } 
            
            health -= value;
            invincCounter = damageInvincLength;
            PlayerController.instance.ChangeAlphaSprite(.5f);
            #region blood
            int selectedSplat = Random.Range(0, deathSplatter.Length);
            int rotation = Random.Range(0, 9);
            //Instantiate(deathSplatter[selectedSplat], transform.position, transform.rotation);
            Instantiate(deathSplatter[selectedSplat], PlayerController.instance.transform.position, Quaternion.Euler(0, 0, rotation * 90f));
            #endregion

        }
    }

    public void CheckPlayerHealth()
    {

        if (health <= 0 && !isDead)
        {
            //sfx
            AudioManager.instance.PlaySFX(11);
            isDead = true;
            //player dies
            AudioManager.instance.PlayGameOverMusic();

            PlayerController.instance.gameObject.SetActive(false);
            UIController.instance.deathUI.SetActive(true);


        }


    }

    #region getters
    public float GetHealth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    #endregion


    #region setters
    public void SetInvincibilityCounter(float value)
    {
        invincCounter = value;
    }
    public void SetHealth(float value)
    {
        health = value;
    }
    #endregion

}
