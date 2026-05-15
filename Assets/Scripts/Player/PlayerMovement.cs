using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerSize")]
    public Vector2 normalSize = new Vector2(0.2f, 0.2f); // Player normal size (size 1)
    public Vector2 mediumSize = new Vector2(0.4f, 0.4f); // Player medium size (size 2)
    public Vector2 largeSize = new Vector2(0.7f, 0.7f); // Player large size (size 3)
    public Vector2 veryLargeSize = new Vector2(1.2f, 1.2f); // Player largest size (size 4)
    public Vector2 smallSize = new Vector2(0.1f, 0.1f); // Player small size (size 0)
    public int playerSize = 1; // Player default size (Normal size)

    [Header("Momvement Speed")]
    public float walkSpeed = 3f;
    public float walkSpeedMultiplier = 2f;
    public int jumpForce = 85;
    public int dashForce = 150;
    private float moveX;
    private float moveY;

    [Header("Interaction Radius")]
    [SerializeField] private float groundCheckRadius = 0.6f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask eatableLayer;

    [Header("Player item lists")]
    private PlayerInventory inventory; // Player inventory

    [Header("Tube Script")]
    private tubeTrail playerTrail;

    [Header("Animation")]
    public Animator animator;
    
    private bool isGround; // Player is on the ground
    private bool isShrinking; // Player Force Shrink
    private bool isRunning; // Player is walking
    public bool isPuke; // Player is puking
    public bool isEating; // Player is eating
    public bool isTubing; // Player is climbing in the tube

    private Rigidbody2D playerRB;
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        inventory = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<PlayerInventory>();
        playerTrail = GetComponent<tubeTrail>();
    }
    private void Update()
    {
        // Ground Detection
        isGround = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer) 
            || Physics2D.OverlapCircle(transform.position, groundCheckRadius, eatableLayer);

        // Jump
        if (isGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            animator.SetBool("IsJump", true);
        }
        if(isGround == true && Input.GetKeyDown(KeyCode.Space) == false)
        {
            animator.SetBool("IsJump", false);
        }

        // Player Scaling
        ScaleUp();
        ForceScaleDown();

        // Player Eating Object Animation
        if(isEating == true)
        {
            animator.SetBool("IsEat", true);
        }
        else
        {
            animator.SetBool("IsEat", false);
        }

        // Player in tube
        if(isTubing == true)
        {
            Tubing();
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
    private void FixedUpdate()
    {
        // Walk
        Walk();
        // Run
        Sprint();

        if (isPuke == false)
        {
            playerRB.velocity = new Vector2(moveX, playerRB.velocity.y);
        }
    }
    private void ScaleUp()
    {
        if (playerSize == 2)
        {
            transform.localScale = mediumSize;
        }
        else if (playerSize == 3)
        {
            transform.localScale = largeSize;
            groundCheckRadius = 1f;
        }
        else if (playerSize == 4)
        {
            transform.localScale = veryLargeSize;
            groundCheckRadius = 1.9f;
        }
    }
    private void ForceScaleDown()
    {
        if(Input.GetKey(KeyCode.C) && isShrinking == false && inventory.foodCount == 0 && playerSize == 1)
        {
            playerSize = 0;
            transform.localScale = smallSize;
            isShrinking = true;
            Debug.Log("Meow");
        }
        else if(Input.GetKeyUp(KeyCode.C) && isShrinking == true && inventory.foodCount == 0)
        {
            playerSize = 1;
            transform.localScale = normalSize;
            isShrinking = false;
        }
    }
    public IEnumerator Dash()
    {
        if (transform.GetComponent<SpriteRenderer>().flipX == true)
        {
            playerRB.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }
        else
        {
            playerRB.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.5f);
        isPuke = false;
    }
    private void Walk()
    {
        moveX = Input.GetAxisRaw("Horizontal") * walkSpeed;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.D) && isRunning == false)
            {
                transform.GetComponent<SpriteRenderer>().flipX = true;
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsRun", false);
            }
            else if (Input.GetKey(KeyCode.D) && isRunning == false)
            {
                transform.GetComponent<SpriteRenderer>().flipX = false;
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsRun", false);
            }
        }
        else
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", false);
        }
    }
    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            moveX = Input.GetAxis("Horizontal") * (walkSpeed * walkSpeedMultiplier);
            if (Input.GetKey(KeyCode.A))
            {
                transform.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.GetComponent<SpriteRenderer>().flipX = false;
            }
            animator.SetBool("IsWalk", false);
            animator.SetBool("IsRun", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
    }
    private void Jump()
    {
        playerRB.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
    }
    private void CancleEat()
    {
        isEating = false;
    }
    private void Tubing()
    {
        moveY = Input.GetAxis("Vertical") * walkSpeed;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            playerTrail.makeTrail = true;
            playerRB.velocity = new Vector2(moveX, moveY);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270));
            playerTrail.makeTrail = true;
            playerRB.velocity = new Vector2(moveX, moveY);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            playerTrail.makeTrail = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            playerTrail.makeTrail = true;
        }
        else
        {
            playerTrail.makeTrail = false;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Vent")
        {
            isTubing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Vent")
        {
            isTubing = false;
            playerTrail.makeTrail = false;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
