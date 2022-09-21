using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CoinType
{
    Bronze,
    Silver,
    Gold
}
public class CoinItem : MonoBehaviour
{
    [SerializeField]
    private int Coin_amount = 0;
    [SerializeField]
    private CoinType Coin_type = CoinType.Bronze;
    public int sfxCoinPickUp;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            AudioManager.instance.PlaySFX(sfxCoinPickUp);
            Destroy(gameObject);

        }
    }



}
