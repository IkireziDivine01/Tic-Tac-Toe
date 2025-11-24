using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public bool SoundOn = true;
    public int AIDifficulty = 1;
    public int GameModeIndex = 0; // 0 = HvsH, 1 = HvsAI

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    public void LoadSettings()
    {
        SoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        AIDifficulty = PlayerPrefs.GetInt("AIDifficulty", 1);
        GameModeIndex = PlayerPrefs.GetInt("GameModeIndex", 0);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("SoundOn", SoundOn ? 1 : 0);
        PlayerPrefs.SetInt("AIDifficulty", AIDifficulty);
        PlayerPrefs.SetInt("GameModeIndex", GameModeIndex);
        PlayerPrefs.Save();
    }

    public void SetGameMode(int index)
    {
        GameModeIndex = index;
        SaveSettings();
    }

    public void SetSound(bool val)
    {
        SoundOn = val;
        SaveSettings();
    }

    public void SetDifficulty(float val)
    {
        AIDifficulty = Mathf.RoundToInt(val);
        SaveSettings();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
