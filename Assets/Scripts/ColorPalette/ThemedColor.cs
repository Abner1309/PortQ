using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Graphic))]
public class ThemedColor : MonoBehaviour
{
    [SerializeField] private ColorToken token;
    [SerializeField] private ColorPalette pallet;

    private Graphic graphic;

    private void OnEnable() { Apply(); }
    private void OnValidate() { Apply(); }

    public void Apply()
    {
        if (pallet == null) return;
        if (graphic == null) graphic = GetComponent<Graphic>();
        graphic.color = pallet.Get(token);
    }
}