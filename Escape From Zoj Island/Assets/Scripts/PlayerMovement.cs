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

    [SerializeField] private float runSpeed = 5.0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        bool isMoving = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
}
