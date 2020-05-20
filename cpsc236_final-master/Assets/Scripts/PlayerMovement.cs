using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public int jumpPotionModAmount = 0;
    public float speedMod = 1.3f;

    public AudioClip jumpClip;


    private float potionTimeMaxJ = 10f;
    private float potionTimeCurJ = 0f;

    private float potionTimeMaxS = 10f;
    private float potionTimeCurS = 0f;

    float horizontalMove = 0f;
    bool jumpFlag = false;
    bool jump = false;



    // Update is called once per frame
    void Update()
    {
        if (!hasSpeedPotion)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else if (hasSpeedPotion)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * speedMod;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (animator.GetBool("IsJumping") == false)
            {
                animator.SetBool("IsJumping", true);
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
                jump = true;
                
            }
        }
    }

    public void onLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    void FixedUpdate()
    {
        if (hasJumpPotion && potionTimeCurJ < potionTimeMaxJ)
        {
            controller.m_JumpForceMod = jumpPotionModAmount;
            potionTimeCurJ += Time.fixedDeltaTime;
        }
        else
        {
            potionTimeCurJ = 0f;
            controller.m_JumpForceMod = 0;
            hasJumpPotion = false;
        }

        if (hasSpeedPotion && potionTimeCurS < potionTimeMaxS)
        {
            speedMod = 2;
            potionTimeCurS += Time.fixedDeltaTime;
        }
        else
        {
            potionTimeCurS = 0f;
            speedMod = 1;
            hasSpeedPotion = false;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }
    }
}
