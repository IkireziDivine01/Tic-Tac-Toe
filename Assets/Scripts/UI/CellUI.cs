using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CellUI : MonoBehaviour
{
    public int Index;
    public GameController GameController;

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        GameController.OnCellClicked(Index);
    }

    public void SetInteractable(bool v) => _button.interactable = v;
}
