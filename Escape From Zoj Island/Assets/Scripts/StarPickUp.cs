using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip starSfx;

    private GameSessions gameSessions;

    void Awake()
    {
        gameSessions = FindObjectOfType<GameSessions>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameSessions.AddStarsCollected(1);
            AudioSource.PlayClipAtPoint(starSfx, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}