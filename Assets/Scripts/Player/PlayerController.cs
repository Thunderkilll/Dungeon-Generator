using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region params
    [Header("Player physics")]
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody2D rb;

    [Header("Player animations")]
    [SerializeField]
    private Animator anim;

    [Header("Player Sprite")]
    [SerializeField]
    private SpriteRenderer playerGRX;

    private Vector2 moveInput; 
    float currentSpeed;


    #endregion
    #region Slingleton
    public static PlayerController instance;

    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

     
        if (anim == null)
        {
            anim = transform.GetComponent<Animator>();
        }
    }

    private void Start()
    {
        currentSpeed = moveSpeed;
        if (anim == null)
        {
            anim = transform.GetComponent<Animator>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (anim == null)
        {
            anim = transform.GetComponent<Animator>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        MoveInputs();

        MoveAnimation();
    }

    /// <summary>
    /// Function that controlls player idle and walking animation
    /// </summary>
    private void MoveAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            Debug.Log("Move");
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    /// <summary>
    /// Logic to move the player : Horizontal and vertical axis 
    /// </summary>

    private void MoveInputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
         
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        //movement
        //transform.position += new Vector3(moveInput.x , moveInput.y , 0f)* moveSpeed * Time.deltaTime;
        //regidBody 
        MovementSpeed(currentSpeed);
    }

    #region movement speed
    public void MovementSpeed(float speed)
    {
        rb.velocity = moveInput * speed;
    }

    public void ChangeSpeed(float value)
    {
        currentSpeed = value;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    #endregion

    #region sprite player
    public void ChangeAlphaSprite(float value)
    {
        playerGRX.color = new Color(playerGRX.color.r, playerGRX.color.g, playerGRX.color.b, value);
    }
    #endregion




}
