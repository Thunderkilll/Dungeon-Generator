using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 
    [Header("Enemy Health")]
    public float health = 150;
    public float maxHealth; //(at the start 100%)
    
    [Header("Enemy Death & Hit")]
    public GameObject[] deathSplatter;
    public GameObject hitImpact;

    [Header("Enemy stun")]
    public float startStunTime;

    public GameObject stunEffect;
    public int deathSfxIndex;
    public int stunSfxIndex;
    public int hurtSfxIndex;

    #region local props
    float stunTime;
    float max;
    float maxSpeed;
    float moveSpeed;
    #endregion

 
    // Update is called once per frame
    void Start()
    {
        max = health;// 150
        moveSpeed = GetComponent<EnemyController>().GetMoveSpeed();
        maxSpeed = moveSpeed;
    }
    private void EnemyStunned()
    {
        if (stunTime <= 0)
        {
            moveSpeed = maxSpeed;
            GetComponent<EnemyController>().SetMoveSpeed(moveSpeed);
            DeactivateStunnEffect();
        }
        else
        {
            moveSpeed = 0;
            GetComponent<EnemyController>().SetMoveSpeed(moveSpeed);
            
            stunTime -= Time.deltaTime;
        }
    }
    public void DamageEnemy(int value)
    {
        ActivateStunEffect();

        health -= value;
        //Debug.Log("enemy took damage");
        Instantiate(hitImpact, transform.position, transform.rotation);
        maxHealth = (int)(health * 100) / max;
        AudioManager.instance.PlaySFX(hurtSfxIndex);
    }

    public void OnEnemyDeath()
    {
        AudioManager.instance.PlaySFX(deathSfxIndex);
        Debug.Log("Enemy dead , display destroyed animation here");
        Destroy(gameObject);
        int selectedSplat = Random.Range(0, deathSplatter.Length);
        int rotation = Random.Range(0, 4);
        
        Instantiate(deathSplatter[selectedSplat], transform.position, Quaternion.Euler(0, 0, rotation * 90f));
        
    }

    private void ActivateStunEffect()
    {
        stunTime = startStunTime;
        stunEffect.SetActive(true);
    }

    private void DeactivateStunnEffect()
    {
        AudioManager.instance.PlaySFX(stunSfxIndex);
        stunEffect.SetActive(false);
    }

    private void Update()
    {
        EnemyStunned();
    }

    public float Health()
    {
        return health;
    }
}
