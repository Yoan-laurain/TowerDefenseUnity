using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded = false;
    public GameObject GameOverUI;

    private void Start()
    {
        gameEnded = false;
    }
    void Update()
    {
        //Plus de vies
        if (PlayerStat.lives <= 0 && !gameEnded)
        {
            EndGame();          
        }
    }

    void EndGame()
    {
        gameEnded = true;
        GameOverUI.SetActive(true);
    }
}
