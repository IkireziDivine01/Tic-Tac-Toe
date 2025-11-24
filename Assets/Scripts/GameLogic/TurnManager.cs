public class TurnManager
{
    public CellState CurrentPlayer { get; private set; } = CellState.X;

    public void Reset(CellState start = CellState.X) => CurrentPlayer = start;

    public void SwitchTurn()
    {
        CurrentPlayer = (CurrentPlayer == CellState.X) ? CellState.O : CellState.X;
    }
}
