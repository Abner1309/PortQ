using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        // SceneManager.LoadScene("NomeDaCenaDoJogo");
        Debug.Log("Play");
    }

    public void Configurations()
    {
        Debug.Log("Configurations");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
