using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button Btn_Start;
    [SerializeField] private Button Btn_Exit;
    [SerializeField] private string SceneName;

    private void Awake()
    {
        Debug.Log("MainMenu set btns listeners on Awake");
        Btn_Start.onClick.AddListener(btn_start);
        Btn_Exit.onClick.AddListener(btn_exit);
    }

    public void btn_start()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("btn_start Game started!");
        PlayerPrefs.SetString(OWNER.PLAYER.ToString(), "tom");
    }
    public void btn_exit()
    {
        Debug.Log("btn_exit Unity quitted");
        Application.Quit();
    }
}
