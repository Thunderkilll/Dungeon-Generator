
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region props
    [Header("Mele weapon settings")]
 
    private float timeBtwAttacks;
    [SerializeField]
    private float startTimeBtwAttacks;

    [SerializeField]
    private Transform attackPos;

    [SerializeField]
    private float attackRangeX;
    [SerializeField]
    private float attackRangeY;

    [SerializeField]
    private int weaponDamage;


    [SerializeField]
    private LayerMask enemyLayerMask;

    public Animator anim;
    #endregion


    // Update is called once per frame
    void Update()
    {
        CalculateSwingingTime();
    }

    private void CalculateSwingingTime()
    {
        if (timeBtwAttacks <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetTrigger("isAttacking");
                
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position , new Vector2(attackRangeX , attackRangeY),0 , enemyLayerMask);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().DamageEnemy(weaponDamage);
                }
 
            }

          timeBtwAttacks = startTimeBtwAttacks;
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY , 1));
    }
}
