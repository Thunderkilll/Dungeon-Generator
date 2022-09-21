using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public bool hasEffect;
    public float damageAmount;
    //effect
    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;// bullet direction toward player
        direction.Normalize();// normalize all values of direction to 1 and 0 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {    
         
        if (other.tag == "Player")
        {
            //do effect and damage here
            PlayerSurvival.instance.TakeDamage(damageAmount);
        }
        if (other.gameObject.layer == 6)
        {
            Debug.Log("wall e");
            Destroy(gameObject);
        }
    
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
