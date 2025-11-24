using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject quitButton;

    void Start()
    {
#if UNITY_WEBGL
        quitButton.SetActive(false);
#else
        quitButton.SetActive(true);
#endif
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnSettingsClicked()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
