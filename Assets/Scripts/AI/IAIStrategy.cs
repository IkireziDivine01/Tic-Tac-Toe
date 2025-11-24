public interface IAIStrategy
{
    // returns 0..8 or -1 when no move
    int GetAIMove(BoardState board, CellState aiSide);
}
