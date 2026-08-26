using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controla a reprodução de vídeo: play/pause, avançar, retroceder e reiniciar.
/// Deve ser anexado a um GameObject dedicado (ex: VideoManager), com referência
/// ao VideoPlayer configurado com Source = Url (StreamingAssets).
/// </summary>
public class VideoPlayerController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Botões (opcional, para atualizar ícones/texto)")]
    [SerializeField] private TextMeshProUGUI playPauseButtonLabel;

    [Header("Configurações")]
    [SerializeField] private float skipSeconds = 10f; // quanto avança/retrocede por clique

    private bool isPaused = false;

    // Só vira true quando o USUÁRIO clica em pausar (não quando o vídeo termina sozinho).
    // É essa flag que a VideoProgressBar consulta para saber se deve retomar o play após um seek.
    private bool manuallyPaused = false;

    /// <summary>True somente se o usuário pausou manualmente (não conta o fim natural do vídeo).</summary>
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

    /// <summary>
    /// Alterna entre pausar e retomar o vídeo. Ligar este método ao botão único de Play/Pause.
    /// </summary>
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
        UpdateButtonLabel();
    }

    public void Resume()
    {
        videoPlayer.Play();
        isPaused = false;
        manuallyPaused = false;
        UpdateButtonLabel();
    }

    /// <summary>
    /// Avança o vídeo em skipSeconds (padrão 10s). Ligar ao botão "Avançar".
    /// </summary>
    public void FastForward()
    {
        double newTime = videoPlayer.time + skipSeconds;

        // Garante que não ultrapasse a duração total
        if (newTime > videoPlayer.length)
            newTime = videoPlayer.length;

        videoPlayer.time = newTime;
    }

    /// <summary>
    /// Retrocede o vídeo em skipSeconds (padrão 10s). Ligar ao botão "Retroceder".
    /// </summary>
    public void Rewind()
    {
        double newTime = videoPlayer.time - skipSeconds;

        // Garante que não fique negativo
        if (newTime < 0)
            newTime = 0;

        videoPlayer.time = newTime;
    }

    /// <summary>
    /// Reinicia o vídeo do começo, mantendo o estado de play/pause atual.
    /// </summary>
    public void Restart()
    {
        videoPlayer.time = 0;

        if (!isPaused)
        {
            videoPlayer.Play();
        }
    }

    /// <summary>
    /// Vai para um tempo específico (útil se você adicionar uma barra de progresso/Slider depois).
    /// </summary>
    public void SeekTo(float normalizedTime)
    {
        // normalizedTime entre 0 e 1 (ex: valor de um Slider)
        double targetTime = videoPlayer.length * normalizedTime;
        videoPlayer.time = targetTime;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Importante: NÃO marcamos manuallyPaused aqui. O vídeo parou sozinho,
        // não foi o usuário quem pausou — por isso o slider ainda pode retomar o play.
        isPaused = true;
        UpdateButtonLabel();
    }

    private void UpdateButtonLabel()
    {
        if (playPauseButtonLabel == null) return;

        playPauseButtonLabel.text = isPaused ? "Play" : "Pause";
    }
}
