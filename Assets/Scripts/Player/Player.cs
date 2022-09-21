using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    LayerMask obstacleMask;
    [SerializeField]
    float speed;
    Vector2 targetPosition;
    Transform GFX;
    Vector2 hitsize;
    [SerializeField]
    float FlipX;
    float FlipY;
    float horizontal;
    float vertical;
    bool isMoving;

    //check for collision 
    Collider2D hit;


    // Start is called before the first frame update

    private void Awake()
    {
        GFX = GetComponentInChildren<SpriteRenderer>().transform;

    }
    void Start()
    {
        obstacleMask = LayerMask.GetMask("Wall", "Enemy");
        hitsize = Vector2.one * .8f;
        FlipX = GFX.localScale.x;
        FlipY = GFX.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        horizontal = System.Math.Sign(Input.GetAxisRaw("Horizontal"));
        vertical = System.Math.Sign(Input.GetAxisRaw("Vertical"));

        #region direction

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            if (Mathf.Abs(horizontal) > 0)
            {
                GFX.localScale = new Vector2(FlipX * horizontal, GFX.localScale.y);
            }


            if (!isMoving)
            {
                //Debug.Log("");
                //  Debug.Log("<size=14><b>is Moving</b></size>");
                
                if (Mathf.Abs(horizontal) > 0)
                {
                    targetPosition = new Vector2(transform.position.x + horizontal, transform.position.y);

                }
                else if (Mathf.Abs(vertical) > 0)
                {
                    targetPosition = new Vector2(transform.position.x, transform.position.y + vertical);

                }

                hit = Physics2D.OverlapBox(targetPosition, hitsize, 0f, obstacleMask);

                if (!hit)
                {
                    StartCoroutine("SmoothMove");
                }
            }

        }

        #endregion
    }

    IEnumerator SmoothMove()
    {
 
        isMoving = true; 
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        } 
        isMoving = false;
    }
}
