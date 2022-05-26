using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region props
        [Header("Dashing Settings")]
        public float dashSpeed = 8f;
        public float dashLength = 8f;
        public float dashCooldown = 8f;
        public float dashInvincibility = 1f;
       
        float dashCounter = 0f;
        float dashCooldownCounter = 0f;
        float currSpeed;
        Animator anim;
    #endregion

    private void Awake()
    {
        if (anim == null)
        {
            anim = transform.GetComponent<Animator>();
        }
    }
    private void Update()
    {
        DashingInput();
        
    }

    public void DashingInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (dashCooldownCounter <= 0 && dashCounter <=0)
            {
                
                anim.SetTrigger("isDashing");
                currSpeed = PlayerController.instance.GetMoveSpeed(); // get player normal speed
                //  PlayerController.instance.MovementSpeed(dashCounter);
                PlayerController.instance.ChangeSpeed(dashSpeed); //change speed to dash speed
                PlayerSurvival.instance.SetInvincibilityCounter(dashInvincibility);
                dashCounter = dashLength;
              
            }
       
          
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                PlayerController.instance.ChangeSpeed(currSpeed);
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        
        }
    }

    private void InvincibilityWhenHit()
    {
        if (dashInvincibility > 0)
        {
            dashInvincibility -= Time.deltaTime;
            if (dashInvincibility <= 0)
            {
                PlayerController.instance.ChangeAlphaSprite(1f);
            }
        }
    }
}
