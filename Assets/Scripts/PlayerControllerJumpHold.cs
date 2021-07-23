using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJumpHold : MonoBehaviour
{
    [Header("Y Axis Movement")]
    public float jumpSpeed = 45;
    [SerializeField] float fallSpeed = 45;
    [SerializeField] int jumpSteps = 20;
    [SerializeField] int jumpThreshold = 7;

    [Header("Jumping")]
    //public float jumpSpeed = 10f;
    public int extraJump = 0;
    public float jumpTime = 0.4f;
    public float gravityScale = 1.0f;


    [Header("Ground check")]
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private float jumpTimeCounter;

    private bool isJumping;

    // tolerance time for jumping after grounded
    private float defaultLastGroundTime = 0.2f;
    private float lastGroundedTime;


    int stepsJumped = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = gravityScale;
        lastGroundedTime = defaultLastGroundTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        if (!Input.GetButton("Jump") && stepsJumped < jumpSteps && stepsJumped > jumpThreshold && isJumping)
        {
            StopJumpQuick();
        }
        else if (!Input.GetButton("Jump") && stepsJumped < jumpThreshold && isJumping)
        {
            StopJumpSlow();
        }
    }

    void StopJumpQuick()
    {
        //Stops The player jump immediately, causing them to start falling as soon as the button is released.
        stepsJumped = 0;
        isJumping = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private void FixedUpdate()
    {
        // ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        Jump();
    }


    void Jump()
    {
        if (isJumping)
        {

            //if (stepsJumped < jumpSteps && !Roofed())
            if (stepsJumped < jumpSteps)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                stepsJumped++;
            }
            else
            {
                StopJumpSlow();
            }
        }

        //This limits how fast the player can fall
        //Since platformers generally have increased gravity, you don't want them to fall so fast they clip trough all the floors.
        if (rb.velocity.y < -Mathf.Abs(fallSpeed))
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -Mathf.Abs(fallSpeed), Mathf.Infinity));
        }
    }

    void StopJumpSlow()
    {
        //stops the jump but lets the player hang in the air for awhile.
        stepsJumped = 0;
        isJumping = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
