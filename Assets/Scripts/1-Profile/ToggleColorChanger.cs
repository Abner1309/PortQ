using UnityEngine;
using UnityEngine.UI;

public class ToggleColorChanger : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private ColorToken tokenMarked;
    [SerializeField] private ColorToken tokenUnmarked;
    [SerializeField] private ThemedColor themedColor;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(UpdateColor);
        UpdateColor(toggle.isOn);
    }

    private void UpdateColor(bool isOn)
    {
        themedColor.SetToken(isOn ? tokenMarked :  tokenUnmarked);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(UpdateColor);
    }
}
