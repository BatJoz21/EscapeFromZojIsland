using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rbBullet;
    PlayerMovement player;
    float xSpeed;

    [SerializeField] private float bulletSpeed = 0.5f;

    void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        rbBullet.velocity = new Vector2(xSpeed, rbBullet.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemies")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
