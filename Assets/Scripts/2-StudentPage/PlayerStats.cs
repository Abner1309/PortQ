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
	
	private const string LastLoginKey = "LastLogin";
	private const string StreakKey = "Streak";
	private const string DateFormat = "yyyy/MM/dd";

	private void UpdateStreak()
	{
		DateTime today = DateTime.Today;
		int streak = PlayerPrefs.GetInt(StreakKey, 0);

		if (PlayerPrefs.HasKey(LastLoginKey)
			&& DateTime.TryParseExact(
				PlayerPrefs.GetString(LastLoginKey),
				DateFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out DateTime lastDate))
		{
			int daysDiff = (today - lastDate.Date).Days;

			if (daysDiff == 0)
			{
				streak = Mathf.Max(streak, 1);
			}
			else if (daysDiff == 1)
			{
				streak++;
			}
			else
			{
				streak = 1;
			}
		}
		else
		{
			streak = 1;
		}

		PlayerPrefs.SetInt(StreakKey, streak);
		PlayerPrefs.SetString(LastLoginKey, today.ToString(DateFormat, CultureInfo.InvariantCulture));
		PlayerPrefs.Save();

		userStreak.text = streak == 1 ? "1 Dia" : $"{streak} Dias";
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

