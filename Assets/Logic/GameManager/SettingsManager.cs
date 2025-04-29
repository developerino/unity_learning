using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private Button _btnApply;
    [SerializeField] private Button _btnReset;

    private const string SETTINGS_PREF_KEY = "SelectedOption";

    private Action _onCloseCallback;

    private void Awake()
    {
        _btnApply.onClick.AddListener(ApplySettings);
        _btnReset.onClick.AddListener(ResetSettings);
        _dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void Start()
    {
        if (PlayerData.Instance != null)
        {
            int savedOption = PlayerData.PlayerPreferences.GetPref<int>(SETTINGS_PREF_KEY, 0);
            _dropdown.SetValueWithoutNotify(savedOption);
        }
    }

    public void OpenSettings(Action onCloseCallback = null)
    {
        _onCloseCallback = onCloseCallback;
        gameObject.SetActive(true);
    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);
        _onCloseCallback?.Invoke();
    }

    private void ApplySettings()
    {
        int selectedIndex = _dropdown.value;
        PlayerData.PlayerPreferences.SetPref<int>(SETTINGS_PREF_KEY, selectedIndex);

        CloseSettings();
    }

    private void ResetSettings()
    {
        _dropdown.value = 0;
        PlayerData.PlayerPreferences.SetPref<int>(SETTINGS_PREF_KEY, 0);

        CloseSettings();
    }

    private void OnDropdownChanged(int index)
    {
        Debug.Log($"SettingsManager: Dropdown selection changed to {index}");
    }
}
