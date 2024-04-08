using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    [SerializeField] private Sprite[] lifeSprites;
    [SerializeField] private Image lifeHUD;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameSessions gameSessions;
    private int playerLifes;
    private int playerStars;

    void Awake()
    {
        //gameSessions = FindObjectOfType<GameSessions>();
    }

    void Update()
    {
        playerLifes = gameSessions.GetPlayerLifes();
        playerStars = gameSessions.GetStarsCollected();
        TotalLifesUI();
        TotalScoreUI();
    }

    private void TotalLifesUI()
    {
        if (playerLifes == 3)
        {
            lifeHUD.sprite = lifeSprites[0];
        }
        else if (playerLifes == 2)
        {
            lifeHUD.sprite = lifeSprites[1];
        }
        else if (playerLifes == 1)
        {
            lifeHUD.sprite = lifeSprites[2];
        }
        else
        {
            lifeHUD.sprite = lifeSprites[3];
        }
    }

    private void TotalScoreUI()
    {
        int scoreTotal = playerStars * 5;
        scoreText.text = scoreTotal.ToString();
    }
}
