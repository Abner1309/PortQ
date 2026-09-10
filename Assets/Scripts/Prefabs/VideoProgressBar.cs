using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;

public class VideoProgressBar : MonoBehaviour
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

        SetupEventTrigger();
    }

    private void OnEnable()
    {
        progressSlider.minValue = 0f;
        progressSlider.maxValue = 1f;
    }

    private void Update()
    {
        if (!isDragging && videoPlayer.length > 0)
        {
            float normalizedTime = (float)(videoPlayer.time / videoPlayer.length);
            progressSlider.SetValueWithoutNotify(normalizedTime);
        }

        UpdateTimeLabels();
    }

    private void SetupEventTrigger()
    {
        EventTrigger trigger = progressSlider.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = progressSlider.gameObject.AddComponent<EventTrigger>();

        AddTriggerListener(trigger, EventTriggerType.PointerDown, (_) => BeginDrag());
        AddTriggerListener(trigger, EventTriggerType.PointerUp, (_) => FinishDrag());
        AddTriggerListener(trigger, EventTriggerType.BeginDrag, (_) => BeginDrag());
        AddTriggerListener(trigger, EventTriggerType.EndDrag, (_) => FinishDrag());
    }

    private void AddTriggerListener(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
    }

    private void BeginDrag()
    {
        isDragging = true;
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

    private void FinishDrag()
    {
        if (!isDragging) return;

        isDragging = false;

        double targetTime = videoPlayer.length * progressSlider.value;
        videoPlayer.time = targetTime;

        if (wasPlayingBeforeDrag)
        {
            if (playerController != null)
                playerController.Resume();
            else
                videoPlayer.Play();
        }
    }

    public void OnSliderValueChanged(float value)
    {
        if (!isDragging) return;

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
