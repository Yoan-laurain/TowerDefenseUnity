using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded = false;

    [Header("References")]
    public GameObject gameOverUI;
    public GameObject completeLevelUI;

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

        //Ecran de defaite
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        gameEnded = true;
        completeLevelUI.SetActive(true);
    }
}
