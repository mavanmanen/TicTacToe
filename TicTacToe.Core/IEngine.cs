namespace TicTacToe.Core;

public interface IEngine
{
    public GameState State { get; }
    public event EventHandler<Player> OnGameWin;
    public event EventHandler<EventArgs> OnGameOver;
    public void SetCell(int index);
    public void Reset();
}