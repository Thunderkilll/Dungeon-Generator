using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
 
    private SpriteRenderer thisGfx;
    float alpha;
    public float delay = .5f;
    private void Awake()
    {
        thisGfx = GetComponent<SpriteRenderer>();
        alpha = thisGfx.color.a;
    }
    // Update is called once per frame
    void Update()
    {
        if (alpha > 0)
        {
            alpha -= Time.deltaTime * delay;
            ChangeImageAlpha(alpha);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }


    void ChangeImageAlpha(float value)
    {
        thisGfx.color = new Color(thisGfx.color.r, thisGfx.color.g, thisGfx.color.b, value);
    }
}
