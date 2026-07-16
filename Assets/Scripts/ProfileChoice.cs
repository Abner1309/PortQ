using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProfileChoice : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public void TeacherChoice()
    {
        Debug.Log("Teacher Choice");
        SceneManager.LoadScene("3-Create_Classroom");
        // SaveProfileAndLoad("Teacher");
    }

    public void StudentChoice()
    {
        Debug.Log("Student Choice");
        SceneManager.LoadScene("4-Classroom_List");
        // SaveProfileAndLoad("Student");
    }

    private void SaveProfileAndLoad(string profile)
    {
        string nameAccess = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(nameAccess))
        {
            Debug.Log("Digite um nome antes de continuar.");
            return;
        }

        // PlayerPrefs.SetString("NameAccess", nameAccess);
        // PlayerPrefs.SetString("ProfileAccess", profile);
        // PlayerPrefs.Save();

        // SceneManager.LoadScene(nextSceneName);
    }
}
