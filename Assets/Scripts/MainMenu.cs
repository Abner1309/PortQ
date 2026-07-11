using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("2-Profile_Screen");
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
