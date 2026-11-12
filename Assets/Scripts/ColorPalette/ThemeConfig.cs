using UnityEngine;

[CreateAssetMenu(fileName = "ThemeConfig", menuName = "Scriptable Objects/ThemeConfig")]
public class ThemeConfig : ScriptableObject
{
    public ColorPalette defaultPalette;
    public ColorPalette contrastPalette;
}