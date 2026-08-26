using UnityEngine;
using UnityEngine.UI;

public class ToggleColorChanger : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private Color corDesmarcado = Color.white;
    [SerializeField] private Color corMarcado = Color.green;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(AtualizarCor);
        AtualizarCor(toggle.isOn); // Define a cor inicial
    }

    private void AtualizarCor(bool isOn)
    {
        backgroundImage.color = isOn ? corMarcado : corDesmarcado;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(AtualizarCor);
    }
}
