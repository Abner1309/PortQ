using UnityEngine;

public class MenuConfiguration : MonoBehaviour
{
    [SerializeField] private GameObject panelConfig;
    
    private void Update()
    {
    	if (Input.GetKeyDown(KeyCode.Escape) && panelConfig.activeSelf) Close();
    }
    
    public void Open()
    {
    	panelConfig.SetActive(true);
    }
    
    public void Close()
    {
    	panelConfig.SetActive(false);
    }
    
    public void Alternate()
    {
    	panelConfig.SetActive(!panelConfig.activeSelf);
    }
}
