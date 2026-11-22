using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
	[SerializeField] private Image displayedImage;
	[SerializeField] private Sprite[] sprites;
	
	void Start()
	{
		Toggle[] toggles = GetComponentsInChildren<Toggle>();
		
		for (int i = 0; i < toggles.Length && i < sprites.Length; i++)
		{
			int index = i;
			toggles[i].onValueChanged.AddListener((isOn) => 
			{
				if (isOn)
					displayedImage.sprite = sprites[index];
			});
			
			if (toggles[i].isOn)
				displayedImage.sprite = sprites[index];
		}
	}
}
