using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [Header("UI")]
    public Button[] levelButtons;

    private void Start()
    {
        //On r�cup�re le niveau max atteint
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //On parcours tout les boutons de niveau
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Si le niveau est sup�rieur au niveau max attein
            if( i + 1 > levelReached)
            {
                // On le d�sactive pour le joueur
                levelButtons[i].interactable = false;
            }
        }
    }
   
    public void Select(string levelName)
    {
        // On charge la scene 
        SceneManager.LoadScene(levelName);
    }
}
