using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("UI refs")]
    public TMP_Text displayText;
    public Button resetButton;
    public GameObject[] strikeLines;
    public Button[] cellButtons;

    [Header("Sprites")]
    public Sprite spriteX;
    public Sprite spriteO;
    public Sprite spriteEmpty;

    [Header("Game settings")]
    public GameMode gameMode = GameMode.HumanVsHuman;
    public CellState aiSide = CellState.O;

    private BoardState board;
    private TurnManager turns;
    private IAIStrategy aiStrategy;

    void Awake()
    {
        board = new BoardState();
        turns = new TurnManager();
        aiStrategy = new RuleBasedAIStrategy();

        if (cellButtons == null || cellButtons.Length != 9)
            Debug.LogError("Assign 9 cellButtons in GameController inspector.");

        // wire cellUI references on each button if present
        for (int i = 0; i < cellButtons.Length; i++)
        {
            var ui = cellButtons[i].GetComponent<CellUI>();
            if (ui != null) ui.GameController = this;
        }

        resetButton.onClick.AddListener(ResetGame);
    }

    void Start()
    {
        ApplySettings();
        ResetGame();
    }

    public void ApplySettings()
    {
        if (SettingsManager.Instance != null)
        {
            gameMode = (SettingsManager.Instance.GameModeIndex == 0)
                ? GameMode.HumanVsHuman
                : GameMode.HumanVsAI;
            // Optional: you can also use SettingsManager.Instance.AIDifficulty
        }
    }

    public void OnCellClicked(int index)
    {
        var (res, _) = board.CheckResult();
        if (res != GameResult.Ongoing) return;
        if (!board.IsCellEmpty(index)) return;
        if (gameMode == GameMode.HumanVsAI && turns.CurrentPlayer == aiSide) return;

        board.TrySetCell(index, turns.CurrentPlayer);
        UpdateCellVisual(index, turns.CurrentPlayer);
        AfterMove();
    }

    private void AfterMove()
    {
        var (res, winningLine) = board.CheckResult();
        if (res != GameResult.Ongoing)
        {
            EndGame(res, winningLine);
            return;
        }

        turns.SwitchTurn();
        UpdateDisplay();

        if (gameMode == GameMode.HumanVsAI && turns.CurrentPlayer == aiSide)
            StartCoroutine(AIMoveRoutine());
    }

    private IEnumerator AIMoveRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        int move = aiStrategy.GetAIMove(board, aiSide);
        if (move >= 0)
        {
            board.TrySetCell(move, turns.CurrentPlayer);
            UpdateCellVisual(move, turns.CurrentPlayer);
            AfterMove();
        }
    }

    private void EndGame(GameResult result, int[] winningLine)
    {
        displayText.text = result switch
        {
            GameResult.XWins => "Player X Wins",
            GameResult.OWins => "Player O Wins",
            _ => "Draw!"
        };

        if (result == GameResult.XWins || result == GameResult.OWins)
        {
            int idx = LinesToStrikeIndex(winningLine);
            if (idx >= 0 && idx < strikeLines.Length) strikeLines[idx].SetActive(true);
        }

        foreach (var b in cellButtons)
        {
            var ui = b.GetComponent<CellUI>();
            if (ui != null) ui.SetInteractable(false);
            else
            {
                var bb = b.GetComponent<Button>();
                if (bb != null) bb.interactable = false;
            }
        }
    }

    private int LinesToStrikeIndex(int[] winningLine)
    {
        int[][] mapping = new int[][]
        {
            new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8}, // rows
            new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8}, // cols
            new[]{0,4,8}, new[]{2,4,6}               // diags
        };

        for (int i = 0; i < mapping.Length; i++)
        {
            var m = mapping[i];
            if (m[0] == winningLine[0] && m[1] == winningLine[1] && m[2] == winningLine[2]) return i;
        }
        return -1;
    }

    private void UpdateCellVisual(int index, CellState v)
    {
        var btn = cellButtons[index];
        var img = btn.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogWarning($"Cell {index} missing Image component.");
            return;
        }

        img.sprite = v switch
        {
            CellState.X => spriteX,
            CellState.O => spriteO,
            _ => spriteEmpty
        };

        var b = btn.GetComponent<Button>();
        if (b != null) b.interactable = false;
    }

    private void UpdateDisplay()
    {
        displayText.text = turns.CurrentPlayer == CellState.X
            ? "Player X's Turn"
            : "Player O's Turn";
    }

    public void ResetGame()
    {
        ApplySettings();

        board.Reset();
        turns.Reset(CellState.X);
        UpdateDisplay();

        for (int i = 0; i < cellButtons.Length; i++)
        {
            var img = cellButtons[i].GetComponent<Image>();
            if (img != null) img.sprite = spriteEmpty;
            var ui = cellButtons[i].GetComponent<CellUI>();
            if (ui != null) ui.SetInteractable(true);
            else
            {
                var b = cellButtons[i].GetComponent<Button>();
                if (b != null) b.interactable = true;
            }
        }

        foreach (var s in strikeLines) if (s != null) s.SetActive(false);

        if (gameMode == GameMode.HumanVsAI && turns.CurrentPlayer == aiSide)
            StartCoroutine(AIMoveRoutine());
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
