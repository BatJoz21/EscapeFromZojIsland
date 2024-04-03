using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rbEnemy;

    [SerializeField] private float enemyMoveSpeed = 5.0f;

    void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rbEnemy.velocity = new Vector2(-enemyMoveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyMoveSpeed = -enemyMoveSpeed;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(rbEnemy.velocity.x), 1f);
    }
}
