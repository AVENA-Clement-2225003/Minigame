using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] string m_SceneGameName;
    // Start is called before the first frame update
    void StartGame()
    {
        StartCoroutine(LoadYourAsyncScene(m_SceneGameName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void Quit()
    {
#if UNITY_EDITOR
    Application.Quit();
#else
        Debug.Log("Quit");
#endif
    }
}
