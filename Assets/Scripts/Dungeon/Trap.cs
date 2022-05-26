using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    #region props

    public float trapDamage = 1f;
    public float stayTrapDamage = .5f;
    #endregion
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(trapDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(stayTrapDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(trapDamage);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerSurvival.instance.TakeDamage(stayTrapDamage);
        }
    }
}
