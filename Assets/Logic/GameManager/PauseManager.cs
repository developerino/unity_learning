using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _inputBlocker;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _panelSettings;

    [Header("Buttons")]
    [SerializeField] private Button _btnContinuePlaying;
    [SerializeField] private Button _btnLoadGame;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnQuit;

    private bool _isPaused = false;

    private void Awake()
    {
        _pauseMenuUI.SetActive(false);
        _panelSettings.SetActive(false);

        _btnContinuePlaying.onClick.AddListener(OnResumeClicked);
        _btnLoadGame.onClick.AddListener(OnLoadGameClicked);
        _btnSettings.onClick.AddListener(OnSettingsClicked);
        _btnQuit.onClick.AddListener(OnQuitClicked);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused && !_panelSettings.activeSelf)
                Resume();
            else if (_isPaused && _panelSettings.activeSelf)
                ReturnFromSettings();
            else
                Pause();
        }
    }

    private void Pause()
    {
        _inputBlocker.SetActive(true);
        _pauseMenuUI.SetActive(true);
        _isPaused = true;
        InputManager.EnableInput(false);
    }

    private void Resume()
    {
        _inputBlocker.SetActive(false);
        _pauseMenuUI.SetActive(false);
        _isPaused = false;
        InputManager.EnableInput(true);
    }

    private void OnResumeClicked() => Resume();

    private void OnLoadGameClicked()
    {
        Debug.Log("PauseMenu: Load game requested.");
        StartCoroutine(LoadGameAndCleanup());
    }

    private IEnumerator LoadGameAndCleanup()
    {
        if (TransitionManager.Instance != null)
            yield return TransitionManager.Instance.FadeOut(null, 0.5f);

        SaveManager.LoadGame();

        yield return null;

        _inputBlocker.SetActive(false);
        _pauseMenuUI.SetActive(false);
        _isPaused = false;
        Debug.Log("PauseMenu: Load finished, resumed game.");
    }

    private void OnSettingsClicked()
    {
        Debug.Log("PauseMenu: Open settings panel.");
        _pauseMenuUI.SetActive(false);

        if (_panelSettings != null)
        {
            _panelSettings.GetComponent<SettingsManager>().OpenSettings(() =>
            {
                Debug.Log("PauseMenu: Returning from Settings");
                _panelSettings.SetActive(false);
                _pauseMenuUI.SetActive(true);
            });
        }
    }

    private void OnQuitClicked()
    {
        Debug.Log("PauseMenu: Quit button clicked.");
        StartCoroutine(QuitToMainMenuRoutine());
    }

    private IEnumerator QuitToMainMenuRoutine()
    {
        if (TransitionManager.Instance != null)
            yield return TransitionManager.Instance.FadeOut(null, 0.5f);

        _inputBlocker.SetActive(false);
        _pauseMenuUI.SetActive(false);
        _isPaused = false;

        InputManager.EnableInput(false); // No input while changing scene
        SceneManager.LoadScene("MainMenu");
    }

    private void ReturnFromSettings()
    {
        _panelSettings.SetActive(false);
        _pauseMenuUI.SetActive(true);
    }
}
