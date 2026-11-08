using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProfileChoice : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public void TeacherChoice()
    {
        Debug.Log("Teacher Choice");
        SaveProfile("Teacher");
        SceneManager.LoadScene("3-Create_Classroom");        
    }

    public void StudentChoice()
    {
        Debug.Log("Student Choice");
        SaveProfile("Student");
        SceneManager.LoadScene("4-Classroom_List");        
    }

    private void SaveProfile(string profile)
    {
        string nameAccess = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(nameAccess))
        {
            Debug.Log("Digite um nome antes de continuar.");
            return;
        }

	PlayerPrefs.SetString("NameAccess", nameAccess);
        PlayerPrefs.SetString("ProfileAccess", profile);
        PlayerPrefs.Save();
    }
}
