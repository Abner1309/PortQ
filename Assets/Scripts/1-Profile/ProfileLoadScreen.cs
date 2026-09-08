using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileLoadScreen : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Toggle studentToggle;
    [SerializeField] private Toggle teacherToggle;

    private void Awake()
    {
        studentToggle.group = toggleGroup;
        teacherToggle.group = toggleGroup;
    }

    public void ButtonEnter()
    {
        if (studentToggle.isOn)
        {
            SceneManager.LoadScene("2-StudentPage");
        }
        if (teacherToggle.isOn)
        {
            SceneManager.LoadScene("3-TeacherPage");
        }
    }
}
