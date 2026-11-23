using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private SceneName nextScene;
    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }
    
    public void LoadScene(SceneName sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}