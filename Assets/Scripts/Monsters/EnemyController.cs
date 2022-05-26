using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region params
    [Header("Enemy Global Settings")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rangeChase;
    [SerializeField]
    private float respawnDelay;
    [SerializeField]
    private bool isDead =false;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer sp;

    private Vector3 moveDirection;

    public EnemyFireMechanic enemyFireMechanic;
    [Header("Enemy attack settings")]

    public float damage;
    public float stayDamage;


    #endregion


    private void Awake()
    {
 
        if (enemyFireMechanic == null)
        {
            enemyFireMechanic = GetComponent<EnemyFireMechanic>();
        }
    }

    void Update()
    {
        if (!isDead)
        {
            if (sp.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
            {
                EnemyMoveDirection();
                EnemyMoveAnimation();
                enemyFireMechanic.EnemyFireAtPlayer(); 
            }

        }
   
    }



    private void EnemyMoveDirection()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeChase)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        moveDirection.Normalize();

        rb.velocity = moveDirection * moveSpeed;
    }
    private void EnemyMoveAnimation()
    {
        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("e_moving", true);
        }
        else
        {
            anim.SetBool("e_moving", false);
        }
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float value)
    {
        moveSpeed = value;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(stayDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(stayDamage);
        }
    }
}
