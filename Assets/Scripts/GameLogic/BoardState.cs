using System;

public class BoardState
{
    public CellState[] cells = new CellState[9];

    public BoardState()
    {
        Reset();
    }

    public void Reset()
    {
        for (int i = 0; i < 9; i++) cells[i] = CellState.Empty;
    }

    public bool TrySetCell(int index, CellState value)
    {
        if (index < 0 || index >= 9) return false;
        if (cells[index] != CellState.Empty) return false;
        cells[index] = value;
        return true;
    }

    public bool IsCellEmpty(int index) => cells[index] == CellState.Empty;

    public System.ValueTuple<GameResult, int[]> CheckResult()
    {
        int[][] lines = new int[][]
        {
            new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
            new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
            new[]{0,4,8}, new[]{2,4,6}
        };

        foreach (var line in lines)
        {
            int a = line[0], b = line[1], c = line[2];
            if (cells[a] != CellState.Empty && cells[a] == cells[b] && cells[b] == cells[c])
            {
                return (cells[a] == CellState.X ? GameResult.XWins : GameResult.OWins, line);
            }
        }

        for (int i = 0; i < 9; i++)
            if (cells[i] == CellState.Empty) return (GameResult.Ongoing, null);

        return (GameResult.Draw, null);
    }

    public BoardState Clone()
    {
        var b = new BoardState();
        for (int i = 0; i < 9; i++) b.cells[i] = this.cells[i];
        return b;
    }
}
