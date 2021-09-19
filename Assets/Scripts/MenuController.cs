using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Method that loads scene by given name
    public void LoadScene(string Name)
    {
        SceneManager.LoadScene(Name);
    }

    public void QuitGame()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            // Close the app in the Unity editor
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            // Close the app on the phone
            Application.Quit();
        }
    }
}
