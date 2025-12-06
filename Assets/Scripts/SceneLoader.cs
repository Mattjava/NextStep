using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Loads a scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Quits the game (works only in build)
    public void QuitGame()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }
}
