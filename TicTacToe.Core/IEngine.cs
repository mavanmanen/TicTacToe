namespace TicTacToe.Core;

public interface IEngine
{
    public event EventHandler<string> OnGameWin;
    public event EventHandler<EventArgs> OnGameOver;
    public void SetCell(int x, int y);
    public void Reset();
}