using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMechanic : MonoBehaviour
{

    #region props
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    bool canFire;
    [SerializeField]
    float fireRate;

    [SerializeField]
    float fireRange;

    float fireCounter;
    [Header("Sound effects")]
    public int index = 13;
    #endregion
   
    public void EnemyFireAtPlayer()
    {
        if (canFire)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= fireRange)
            {

                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    
                    AudioManager.instance.PlaySFX(index);
                    fireCounter = fireRate;
                    Instantiate(bulletPrefab, firePoint.position, transform.rotation);
                }
            }
         
        }
    }
}
