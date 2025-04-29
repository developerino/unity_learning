using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector3> OnWorldClick;

    private bool _inputEnabled = true;

    private void Update()
    {
        if (!_inputEnabled)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                OnWorldClick?.Invoke(hit.point);
            }
        }
    }

    public static void EnableInput(bool enable)
    {
        if (InstanceExists)
        {
            _instance._inputEnabled = enable;
            Debug.Log($"InputManager: Input enabled = {enable}");
        }
    }

    private static InputManager _instance;
    private static bool InstanceExists => _instance != null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
