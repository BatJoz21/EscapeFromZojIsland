using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb2d;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;
    private int platformLayerMask;
    private int ladderLayerMask;
    private float playerGravity;
    private bool isAlive = true;

    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = 5.0f;
    [SerializeField] private float climbSpeed = 5.0f;
    [SerializeField] private Vector2 deathKick = new Vector2(0f, 10f);
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bullet;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        playerGravity = rb2d.gravityScale;
        ladderLayerMask = LayerMask.GetMask("Ladder");
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            ClimbingLadder();
        }
        else
        {
            Death();
        }
    }

    private void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    private void Run()
    {
        if (!isAlive) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;
    }

    private void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        platformLayerMask = LayerMask.GetMask("Ground");
        if (value.isPressed)
        {
            if (boxCollider.IsTouchingLayers(platformLayerMask))
            {
                rb2d.velocity += new Vector2(rb2d.velocity.x, jumpSpeed);
                anim.SetBool("isRunning", false);
                anim.SetBool("isJumping", true);
            }
        }
    }

    private void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }

    private void ClimbingLadder()
    {
        if (!isAlive) { return; }
        bool isClimbing = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;

        if (capsuleCollider.IsTouchingLayers(ladderLayerMask))
        {
            Vector2 playerUp = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
            rb2d.velocity = playerUp;
            rb2d.gravityScale = 0;
            if (isClimbing)
            {
                anim.SetBool("isClimbing", true);
            }
            else
            {
                anim.SetBool("isClimbing", false);
            }
        }
        else
        {
            anim.SetBool("isClimbing", false);
            rb2d.gravityScale = playerGravity;
        }
    }

    private void FlipSprite()
    {
        bool isMoving = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
            anim.SetBool("isRunning", true);
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemies" || collision.collider.tag == "Hazards")
        {
            if (isAlive)
            {
                isAlive = false;
                rb2d.velocity = deathKick;
            }
            isAlive = false;
        }
    }

    private void Death()
    {
        anim.SetTrigger("Dead");
    }
}
