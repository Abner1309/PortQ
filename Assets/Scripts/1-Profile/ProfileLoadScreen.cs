using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class ProfileLoadScreen : MonoBehaviour
{
	private bool nameValid = false;
	private bool classValid = false;

	[Header("Grupo de Botões:")]
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Toggle studentToggle;
    [SerializeField] private Toggle teacherToggle;

	[Header("Campos de Texto (Usuário/Turma):")]
	[SerializeField] private TMP_InputField userName;
	[SerializeField] private TMP_InputField userClassroom;
	
	[Header("Mensagens de Erro:")]
	[SerializeField] private TextMeshProUGUI nameErrorMessage;
	[SerializeField] private TextMeshProUGUI classErrorMessage;

    private void Awake()
    {
        studentToggle.group = toggleGroup;
        teacherToggle.group = toggleGroup;
    }
    
    private void Start()
    {
    	nameErrorMessage.gameObject.SetActive(false);
    	classErrorMessage.gameObject.SetActive(false);
    }
    
    private void VerifyNameField(string str)    
    {
    	string namePattern = @"^[A-Za-z ]+$";
    	nameValid = false; 	
    	
		if (string.IsNullOrEmpty(str))
		{
			nameErrorMessage.text = "O nome do usuário deve ser preenchido.";
			nameErrorMessage.gameObject.SetActive(true);
			return;
		}
		if (str.Length > 40)
		{
			nameErrorMessage.text = "O nome do usuário não deve ter mais do que 40 caracteres.";
			nameErrorMessage.gameObject.SetActive(true);
			return;
		}
		if (!Regex.IsMatch(str, namePattern))
		{
			nameErrorMessage.text = "O nome do usuário deve possuir apenas letras (sem acento e sem cedilha).";
			nameErrorMessage.gameObject.SetActive(true);
			return;
		}
		
		nameErrorMessage.gameObject.SetActive(false);
		nameValid = true;
    }
    
    private void VerifyClassField(string str)
    {
		string classPattern = @"^[0-9][A-Z]$";
		classValid = false;
    	
    	if (string.IsNullOrEmpty(str))
    	{
    		classErrorMessage.text = "O nome da classe deve ser preenchido.";
			classErrorMessage.gameObject.SetActive(true);
    		return;
    	}
    	if (str.Length > 2)
    	{
    		classErrorMessage.text = "O nome da classe deve ter exatamente dois caracteres.";
			classErrorMessage.gameObject.SetActive(true);
    		return;
    	}
    	if (!Regex.IsMatch(str, classPattern))
    	{
    		classErrorMessage.text = "O nome da classe deve possuir exatamento um número seguido de uma letra maiúscula. (Ex.: 6A).";
			classErrorMessage.gameObject.SetActive(true);
	  		return;
    	}    	
    	
    	classErrorMessage.gameObject.SetActive(false);
    	classValid = true;
    }

    public void ButtonEnter()
    {
    	VerifyNameField(userName.text);
    	VerifyClassField(userClassroom.text);
    
    	if (!nameValid || !classValid) return;
    
        if (studentToggle.isOn)
        {
        	PlayerPrefs.SetString("StudentName", userName.text);
        	PlayerPrefs.SetString("StudentClassroom", userClassroom.text);
        	PlayerPrefs.Save();
            SceneManager.LoadScene("2-StudentPage");
        }
        if (teacherToggle.isOn)
        {
        	PlayerPrefs.SetString("TeacherName", userName.text);
        	PlayerPrefs.SetString("TeacherClassroom", userClassroom.text);
        	PlayerPrefs.Save();
            SceneManager.LoadScene("3-TeacherPage");
        }
    }
}
