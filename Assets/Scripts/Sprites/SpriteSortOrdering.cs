using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrdering : MonoBehaviour
{
    SpriteRenderer sp;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        
    }
    void Start()
    {
        sp.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
