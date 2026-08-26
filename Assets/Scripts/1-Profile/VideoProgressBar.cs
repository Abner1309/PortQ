using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;

/// <summary>
/// Barra de progresso de vídeo estilo YouTube.
/// Sincroniza um Slider com o tempo do VideoPlayer, permitindo que o usuário
/// arraste para navegar (scrub) sem conflitar com a atualização automática.
/// </summary>
public class VideoProgressBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Referências")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Slider progressSlider;

    [Header("Controlador (opcional, recomendado)")]
    [Tooltip("Se atribuído, o slider usa o estado de pausa manual do controller em vez de videoPlayer.isPlaying, evitando o bug de ficar travado em pausa quando o vídeo termina.")]
    [SerializeField] private VideoPlayerController playerController;

    [Header("Textos de tempo (opcional)")]
    [SerializeField] private TextMeshProUGUI currentTimeLabel;
    [SerializeField] private TextMeshProUGUI durationLabel;

    private bool isDragging = false;
    private bool wasPlayingBeforeDrag = false;

    private void Awake()
    {
        if (progressSlider == null)
            progressSlider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        // Slider trabalha com valores de 0 a 1 (normalizado)
        progressSlider.minValue = 0f;
        progressSlider.maxValue = 1f;
    }

    private void Update()
    {
        // Só atualiza a posição do slider automaticamente se o usuário
        // não estiver arrastando ele no momento
        if (!isDragging && videoPlayer.length > 0)
        {
            float normalizedTime = (float)(videoPlayer.time / videoPlayer.length);
            progressSlider.SetValueWithoutNotify(normalizedTime);
        }

        UpdateTimeLabels();
    }

    /// <summary>
    /// Chamado quando o usuário começa a tocar/clicar no slider (via evento do sistema).
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        BeginDrag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FinishDrag();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag();
    }

    private void BeginDrag()
    {
        isDragging = true;

        // Usa o estado de PAUSA MANUAL do controller, não videoPlayer.isPlaying.
        // Isso é o que resolve o bug: quando o vídeo termina sozinho, isPlaying fica
        // false, mas manuallyPaused continua false (o usuário não pausou por escolha),
        // então o slider sabe que deve retomar o play ao soltar.
        wasPlayingBeforeDrag = playerController != null
            ? !playerController.IsManuallyPaused
            : videoPlayer.isPlaying;

        if (wasPlayingBeforeDrag)
        {
            if (playerController != null)
                playerController.Pause();
            else
                videoPlayer.Pause();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        FinishDrag();
    }

    private void FinishDrag()
    {
        if (!isDragging) return;

        isDragging = false;

        // Aplica o tempo baseado na posição final do slider
        double targetTime = videoPlayer.length * progressSlider.value;
        videoPlayer.time = targetTime;

        // Retoma a reprodução se estava tocando (ou tinha acabado de terminar) antes do drag
        if (wasPlayingBeforeDrag)
        {
            if (playerController != null)
                playerController.Resume();
            else
                videoPlayer.Play();
        }
    }

    /// <summary>
    /// Ligar este método ao evento On Value Changed do Slider no Inspector,
    /// para permitir clique direto em qualquer ponto da barra (sem precisar arrastar).
    /// </summary>
    public void OnSliderValueChanged(float value)
    {
        if (!isDragging) return; // evita loop com a atualização automática do Update()

        double targetTime = videoPlayer.length * value;
        videoPlayer.time = targetTime;
    }

    private void UpdateTimeLabels()
    {
        if (currentTimeLabel != null)
            currentTimeLabel.text = FormatTime(videoPlayer.time);

        if (durationLabel != null)
            durationLabel.text = FormatTime(videoPlayer.length);
    }

    private string FormatTime(double seconds)
    {
        if (double.IsNaN(seconds) || seconds < 0)
            seconds = 0;

        int minutes = Mathf.FloorToInt((float)seconds / 60f);
        int secs = Mathf.FloorToInt((float)seconds % 60f);
        return $"{minutes:00}:{secs:00}";
    }
}
