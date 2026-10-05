using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudentToggleColor : MonoBehaviour
{
	[Header("Lista de Toggle")]
	[SerializeField] private ToggleGroup toggleGroup;
	[SerializeField] private Toggle toggleMaze;
	[SerializeField] private Toggle toggleAppearances;
	[SerializeField] private Toggle toggleTower;
	[SerializeField] private Toggle toggleConcil;
	[SerializeField] private Toggle toggleEchos;

	private void Awake()
	{
		toggleMaze.group = toggleGroup;
		toggleAppearances.group = toggleGroup;
		toggleTower.group = toggleGroup;
		toggleConcil.group = toggleGroup;
		toggleEchos.group = toggleGroup;
	}

    public void ButtonEnter()
    {
    	if (toggleMaze.isOn) { SceneManager.LoadScene("4-Maze"); }
    	else if (toggleAppearances.isOn) { Debug.Log("Nível 2 Não Implementado."); }
    	else if (toggleTower.isOn) { Debug.Log("Nível 3 Não Implementado."); }
    	else if (toggleConcil.isOn) { Debug.Log("Nível 4 Não Implementado."); }
	 	else if (toggleEchos.isOn) { Debug.Log("Nível 5 Não Implementado."); }
	 	else { Debug.Log("Error"); }
    }
}
