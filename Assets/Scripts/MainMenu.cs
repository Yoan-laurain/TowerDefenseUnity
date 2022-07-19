using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainScene";
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }   
    
    public void Quit()
    {
        Application.Quit(); 
    }
}
