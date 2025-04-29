using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    [Header("Default Fade Settings")]
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _defaultFadeDuration = 1f;
    [SerializeField] private Color _fadeColor = Color.black;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (_fadeImage != null)
        {
            _canvasGroup = _fadeImage.GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
                _canvasGroup = _fadeImage.gameObject.AddComponent<CanvasGroup>();

            _fadeImage.color = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 1f); // FULL BLACK
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _fadeImage.gameObject.SetActive(true);
        }
    }

    public Coroutine FadeOut(System.Action onComplete = null, float? customDuration = null)
    {
        if (_fadeImage == null) return null;
        _fadeImage.gameObject.SetActive(true);
        _canvasGroup.blocksRaycasts = true;
        return StartCoroutine(FadeRoutine(0f, 1f, onComplete, customDuration ?? _defaultFadeDuration));
    }

    public Coroutine FadeIn(System.Action onComplete = null, float? customDuration = null)
    {
        if (_fadeImage == null) return null;
        _fadeImage.gameObject.SetActive(true);
        _canvasGroup.blocksRaycasts = true;
        return StartCoroutine(FadeRoutine(1f, 0f, () =>
        {
            _fadeImage.gameObject.SetActive(false);
            _canvasGroup.blocksRaycasts = false;
            onComplete?.Invoke();
        }, customDuration ?? _defaultFadeDuration));
    }

    private IEnumerator FadeRoutine(float fromAlpha, float toAlpha, System.Action onComplete, float duration)
    {
        if (_fadeImage == null)
        {
            Debug.LogWarning("TransitionManager: Fade Image not assigned.");
            yield break;
        }

        float timer = 0f;
        Color color = _fadeColor;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, t);
            color.a = alpha;
            _fadeImage.color = color;
            _canvasGroup.alpha = alpha;
            yield return null;
        }

        color.a = toAlpha;
        _fadeImage.color = color;
        _canvasGroup.alpha = toAlpha;

        onComplete?.Invoke();
    }
}
