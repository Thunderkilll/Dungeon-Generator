using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMechanic : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField]
    float timeBetweenShots = 0f; 
    //[SerializeField]
    //int bulletCount ;

    float fireCounter;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //fire projectile 
            GameObject go = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            go.name = bulletPrefab.name;
            fireCounter = timeBetweenShots;
        }
        if (Input.GetMouseButton(0))
        {
            fireCounter  -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                GameObject go = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                go.name = bulletPrefab.name;
                fireCounter = timeBetweenShots;
            }
        }
    }
}
