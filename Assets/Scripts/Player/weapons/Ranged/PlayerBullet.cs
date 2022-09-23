using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField]
    float speed = 7.5f;
    [SerializeField]
    int damage;
    [SerializeField]
    GameObject bulletEffect;
    [SerializeField]
    Rigidbody2D rb;

    [Header("Animation Settings")] public int indexSFX;


    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       //hitting a wall
        if (other.gameObject.layer == 6)
        {
            Instantiate(bulletEffect, transform.position, transform.rotation);
           // Debug.Log("you hit wall : <color=red>"+other.name+"</color>");
            Destroy(gameObject);
        }
        //hitting an enemy
        if (other.gameObject.layer == 7)
        {
            other.GetComponent<Enemy>().DamageEnemy(damage);
            Instantiate(bulletEffect, transform.position, transform.rotation);
            //Debug.Log("you hit Enemy : <color=blue>" + other.name + "</color>");

            Destroy(gameObject);
        }
    
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
