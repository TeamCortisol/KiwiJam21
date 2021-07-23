using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;
    public int extraJump = 0;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private int jumpCounter;
    private float defaultLastGroundTime = 0.2f;
    private float lastGroundedTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        jumpCounter = extraJump;
        lastGroundedTime = defaultLastGroundTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Update()
    {
        if (isGrounded)
        {
            //animator.SetBool("isJumping", false);
            //reset counter
            jumpCounter = extraJump;
            lastGroundedTime = defaultLastGroundTime;
        }
        Debug.Log(isGrounded);

        
        lastGroundedTime -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && jumpCounter > 0)
        {
            Debug.Log("jump");
            //animator.SetBool("isJumping", true);
            //AudioManager.Instance.Play("Jump");
            rb.velocity = Vector2.up * jumpSpeed;
            jumpCounter--;
        }
        else if (Input.GetButtonDown("Jump") && jumpCounter == 0 && lastGroundedTime > 0 /*isGrounded*/)
        {
            Debug.Log("jump 2");
            lastGroundedTime = 0;
            //animator.SetBool("isJumping", true);
            //AudioManager.Instance.Play("Jump");
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
