using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    [Header("UI Health")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    [Header("UI Stamina")]
    public Slider staminaSlider;
    [Header("UI Death")]
    public GameObject deathUI;

    float health, maxHealth;
    // Start is called before the first frame update

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
    void Start()
    {
        health    = PlayerSurvival.instance.GetHealth();
        maxHealth = PlayerSurvival.instance.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        UpdateUIStamina();
    }

    private void UpdateUIStamina()
    {
        staminaSlider.value = PlayerSurvival.instance.stamina / PlayerSurvival.instance.maxStamina;
    }

    private void UpdateHealthUI()
    {
        health = PlayerSurvival.instance.GetHealth();
        maxHealth = PlayerSurvival.instance.GetMaxHealth();
        healthSlider.value = health / maxHealth;
        healthText.text = (int)health + "/" + maxHealth;
    }
}
