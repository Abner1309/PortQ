using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScreen : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("1-Profile");
    }
}

