using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessions : MonoBehaviour
{
    [SerializeField] private int playerLifes = 3;
    [SerializeField] private int starsCollected = 0;

    void Awake()
    {
        int numGameSess = FindObjectsOfType<GameSessions>().Length;
        if (numGameSess > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
    }

    public void ProcessPlayerDeath()
    {
        if (playerLifes > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
        Debug.Log(playerLifes);
    }

    private void TakeLife()
    {
        this.playerLifes--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public int GetPlayerLifes() { return this.playerLifes; }

    public int GetStarsCollected() {  return this.starsCollected; }

    public void AddStarsCollected(int star) { this.starsCollected += star; }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
