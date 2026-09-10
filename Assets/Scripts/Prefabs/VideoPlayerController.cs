using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Botão Play/Pause")]
    [SerializeField] private Image playPauseButtonImage;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite pauseSprite;

    [Header("Configurações")]
    [SerializeField] private float skipSeconds = 10f; // quanto avança/retrocede por clique

    private bool isPaused = false;
    private bool manuallyPaused = false;
    public bool IsManuallyPaused => manuallyPaused;

    private void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    public void TogglePlayPause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!videoPlayer.isPlaying) return;

        videoPlayer.Pause();
        isPaused = true;
        manuallyPaused = true;
        UpdateButtonVisual();
    }

    public void Resume()
    {
        videoPlayer.Play();
        isPaused = false;
        manuallyPaused = false;
        UpdateButtonVisual();
    }

    public void FastForward()
    {
        double newTime = videoPlayer.time + skipSeconds;

        if (newTime > videoPlayer.length)
            newTime = videoPlayer.length;

        videoPlayer.time = newTime;
    }

    public void Rewind()
    {
        double newTime = videoPlayer.time - skipSeconds;

        if (newTime < 0)
            newTime = 0;

        videoPlayer.time = newTime;
    }

    public void Restart()
    {
        videoPlayer.time = 0;

        if (!isPaused)
        {
            videoPlayer.Play();
        }
    }

    public void SeekTo(float normalizedTime)
    {
        double targetTime = videoPlayer.length * normalizedTime;
        videoPlayer.time = targetTime;
    }

    public void ToggleMaximize()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        isPaused = true;
        UpdateButtonVisual();
    }

    private void UpdateButtonVisual()
    {
        if (playPauseButtonImage != null)
        {
            playPauseButtonImage.sprite = isPaused ? playSprite : pauseSprite;
        }
    }
}