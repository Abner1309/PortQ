using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Graphic))]
public class ThemedColor : MonoBehaviour
{
    [SerializeField] private ColorToken token;

    private Graphic graphic;

    private void OnEnable()
    {
        Theme.Changed += Apply;
        Apply();
    }

    private void OnDisable() { Theme.Changed -= Apply; }

    private void OnValidate() { Apply(); }

    public void Apply()
    {
        ColorPalette palette = Theme.Current;
        if (palette == null) return;

        if (graphic == null) graphic = GetComponent<Graphic>();
        graphic.color = palette.Get(token);
    }
}