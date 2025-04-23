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
        Debug.Log("Awake");
        Btn_Start.onClick.AddListener(btn_start);
        Btn_Exit.onClick.AddListener(btn_exit);
    }
    void Start()
    {
        Debug.Log("Started");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
        }
    }

    public void btn_start()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Game started!");
        PlayerPrefs.SetString(OWNER.PLAYER.ToString(), "tom");
    }
    public void btn_exit()
    {
        Debug.Log("Unity quitted");
        Application.Quit();
    }
}
