using System;
using System.Linq;

public class RuleBasedAIStrategy : IAIStrategy
{
    private Random _rand = new Random();

    // One-move lookahead: win -> block -> random
    public int GetAIMove(BoardState board, CellState aiSide)
    {
        var avail = Enumerable.Range(0,9).Where(i => board.cells[i] == CellState.Empty).ToArray();
        if (avail.Length == 0) return -1;

        CellState opp = (aiSide == CellState.X) ? CellState.O : CellState.X;

        // 1) winning move
        foreach (var idx in avail)
        {
            var clone = board.Clone();
            clone.TrySetCell(idx, aiSide);
            var (res, _) = clone.CheckResult();
            if ((aiSide == CellState.X && res == GameResult.XWins) || (aiSide == CellState.O && res == GameResult.OWins))
                return idx;
        }

        // 2) block opponent
        foreach (var idx in avail)
        {
            var clone = board.Clone();
            clone.TrySetCell(idx, opp);
            var (res, _) = clone.CheckResult();
            if ((opp == CellState.X && res == GameResult.XWins) || (opp == CellState.O && res == GameResult.OWins))
                return idx;
        }

        // 3) random fallback
        return avail[_rand.Next(avail.Length)];
    }
}
