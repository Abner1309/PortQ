using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Scriptable Objects/ColorPalette")]
public class PrimaryPallete : ScriptableObject
{
    [Header("Cores dos Componentes")]
    public Color title            = Hex("#fdc436");
    public Color explanation      = Hex("#5ddae0");
    public Color instruction      = Hex("#74e791");
    public Color question         = Hex("#ff9296");
    public Color result           = Hex("#f4b93c");
    public Color arcane_panel     = Hex("#eb84ff");
    public Color accent           = Hex("#e7ac2a");
    public Color primary          = Hex("#8843db");
    public Color secondary        = Hex("#1f1e38");
    public Color background       = Hex("#080715");
    public Color card             = Hex("#100f22");
    public Color edge             = Hex("#332d4e99");
    public Color main_text        = Hex("#f4eee0");
    public Color secondary_text   = Hex("#afa390");
    public Color destructive      = Hex("#e60023");
    public Color golden_glow      = Hex("#ffd866");
    public Color arcane           = Hex("#b455ff");
    public Color ember            = Hex("#ff5223");
    public Color field            = Hex("#262540");
    public Color focus            = Hex("#af61f8");

    private static Color Hex(string hex)
    {
        return ColorUtility.TryParseHtmlString(hex, out Color c) ? c : Color.magenta;
    }
}
