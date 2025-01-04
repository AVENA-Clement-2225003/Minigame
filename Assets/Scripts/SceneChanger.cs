using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Méthode publique pour changer de scène
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    // Méthode pour quitter l'application
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Affiché uniquement dans l'éditeur
        Application.Quit();
    }

}
