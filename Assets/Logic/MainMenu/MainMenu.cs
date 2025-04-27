using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _btnNewGame;
    [SerializeField] private Button _btnLoadGame;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnExit;

    private void Awake()
    {
        Debug.Log("MainMenu: Setting up button listeners");
        _btnNewGame.onClick.AddListener(OnNewGameClicked);
        _btnLoadGame.onClick.AddListener(OnLoadGameClicked);
        _btnSettings.onClick.AddListener(OnSettingsClicked);
        _btnExit.onClick.AddListener(OnExitClicked);
    }

    private void OnNewGameClicked()
    {
        Debug.Log("MainMenu: New Game button clicked, loading Battle scene...");
        SceneManager.LoadScene("Battle");
    }

    private void OnLoadGameClicked()
    {
        Debug.Log("MainMenu: Load Game button clicked, loading saved game...");
        SaveManager.LoadGame();
    }

    private void OnSettingsClicked()
    {
        Debug.Log("MainMenu: Settings button clicked");

        // TODO: Show Settings UI (not load new scene! Overlay is better for in-game Settings)
    }

    private void OnExitClicked()
    {
        Debug.Log("MainMenu: Exit button clicked, quitting game...");
        Application.Quit();
    }
}
