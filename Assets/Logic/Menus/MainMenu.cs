using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Buttons")]
    [SerializeField] private Button _btnNewGame;
    [SerializeField] private Button _btnLoadGame;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnExit;

    [Header("Settings Panel (Prefab instance)")]
    [SerializeField] private GameObject _panelSettings;

    private void Awake()
    {
        _btnNewGame.onClick.AddListener(OnNewGameClicked);
        _btnLoadGame.onClick.AddListener(OnLoadGameClicked);
        _btnSettings.onClick.AddListener(OnSettingsClicked);
        _btnExit.onClick.AddListener(OnExitClicked);

        _panelSettings.SetActive(false);
    }

    private void OnNewGameClicked()
    {
        SceneManager.LoadScene("Battle");
    }

    private void OnLoadGameClicked()
    {
        SaveManager.LoadGame();
    }

    private void OnSettingsClicked()
    {
        Debug.Log("MainMenu: Settings button clicked");

        _panelSettings.GetComponent<SettingsManager>().OpenSettings(() =>
        {
            // After closing settings, re-enable main buttons (Main Menu)
            Debug.Log("MainMenu: Returning from Settings");
            _panelSettings.SetActive(false); // (already done in CloseSettings internally but safe)
        });
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }
}
