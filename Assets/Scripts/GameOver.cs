using UnityEngine;

using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Properties")]

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public void OnEnable()
    {
        // Si le niveau qui vient d'être finit est supérieur au plus haut niveau atteint 
        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            // On set up le nouveau niveau débloquer
            PlayerPrefs.SetInt("LevelReached", levelToUnlock);
        }
    }

    public void Retry()
    {
        // Relance la scene active
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
