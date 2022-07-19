using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Properties")]
    public string levelToLoad = "LevelSelector";

    public void Play()
    {
        // On charge la selection de niveau
        SceneManager.LoadScene(levelToLoad);
    }   
    
    public void Quit()
    {
        // On ferme le jeu
        Application.Quit(); 
    }
}
