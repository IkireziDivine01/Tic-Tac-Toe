using UnityEngine;
using UnityEngine.UI; // or TMPro if using TextMeshPro
using TMPro;

public class DifficultyLabelUpdater : MonoBehaviour
{
    public Slider difficultySlider;
    public TextMeshProUGUI label; // use Text if not using TMP

    void Start()
    {
        // Initialize label
        UpdateLabel(difficultySlider.value);

        // Add listener to slider
        difficultySlider.onValueChanged.AddListener(UpdateLabel);
    }

    void UpdateLabel(float value)
    {
        label.text = "Difficulty: " + Mathf.RoundToInt(value);
    }
}
