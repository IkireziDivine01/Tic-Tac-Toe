using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUIInitializer : MonoBehaviour
{
    public TMP_Dropdown gameModeDropdown;
    public Slider difficultySlider;
    public Toggle soundToggle;

    void Start()
    {
        gameModeDropdown.value = SettingsManager.Instance.GameModeIndex;
        difficultySlider.value = SettingsManager.Instance.AIDifficulty;
        soundToggle.isOn = SettingsManager.Instance.SoundOn;
    }
}
