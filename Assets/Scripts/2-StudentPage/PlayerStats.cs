using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class PlayerStats : MonoBehaviour
{
	[Header("Campos de Texto")]
	[SerializeField] private TextMeshProUGUI userName;
	[SerializeField] private TextMeshProUGUI userTag;
	[SerializeField] private TextMeshProUGUI userExp;
	[SerializeField] private TextMeshProUGUI userStreak;
	[SerializeField] private TextMeshProUGUI userMissions;
	[SerializeField] private TextMeshProUGUI percentageMissions;
	
	[Header("Slider")]
	[SerializeField] private Slider sliderMissions;
	
	private void Start()
	{
		UpdateWelcome();
		UpdateSlider();
		UpdateStats(sliderMissions.value);
		UpdateStreak();
	}
	
	private void OnEnable()
	{
		sliderMissions.onValueChanged.AddListener(UpdateStats);
	}
	
	private void OnDisable()
	{
		sliderMissions.onValueChanged.RemoveListener(UpdateStats);
	}
	
	private void UpdateStats(float value)
	{
		int experience = 400 * (int)value;
		userExp.text = $"{experience} PTS";
		userMissions.text = $"{(int)value} de 25 Missões";
		
		float percentage = (value / 25f) * 100f;
		percentageMissions.text = $"{percentage:F0} %";
		
		UpdateTag(experience);
	}
	
	private string GetLastLoginDate()
	{
		if (PlayerPrefs.HasKey("LastLogin"))
		{
			return PlayerPrefs.GetString("LastLogin");
		}
		DateTime today = DateTime.Today;
		string todayDate = today.ToString("yyyy/MM/dd");
		PlayerPrefs.SetString("LastLogin", todayDate);
		return todayDate;
	}
	
	private void UpdateStreak()
	{		
		DateTime actualDate = DateTime.Today;
		DateTime lastDate = DateTime.ParseExact(GetLastLoginDate(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
		
		TimeSpan dateDiff = actualDate.Date - lastDate.Date;
		int daysDiff = dateDiff.Days;	
		
		string strActualDate = actualDate.ToString("yyyy/MM/dd");
		PlayerPrefs.SetString("LastLogin", strActualDate);
		
		if (daysDiff > 1)
		{			
			userStreak.text = $"1 Dia";
		}
		else if (daysDiff == 1)
		{
			string aux = userStreak.text;
			string[] streakParts = aux.Split(' ');
			int quantity = int.Parse(streakParts[0]);
			quantity++;
			userStreak.text = $"{quantity} Dias";
		}	
	}
	
	private void UpdateTag(int value)
	{
		if (value < 500)
		{
			userTag.text = "Aprendiz";
		}
		else if (value < 1500)
		{
			userTag.text = "Escriba";
		}
		else if (value < 3000)
		{
			userTag.text = "Cronista";
		}
		else if (value < 5000)
		{
			userTag.text = "Bardo";
		}
		else if (value < 7500)
		{
			userTag.text = "Contador de Histórias";
		}
		else
		{
			userTag.text = "Mestre dos Gêneros";
		}
	}
	
	private void UpdateSlider()
	{
		sliderMissions.minValue = 0;
		sliderMissions.maxValue = 25;
		sliderMissions.value = 0;
	}
	
	private void UpdateWelcome()
	{
		string name = PlayerPrefs.GetString("StudentName");
		userName.text = $"Olá, {name}!";
	}   
}

