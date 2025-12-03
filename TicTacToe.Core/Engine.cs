namespace TicTacToe.Core;

public class Engine : IEngine
{
    public GameState State { get; private set; } = new();

    public event EventHandler<Player>? OnGameWin;
    public event EventHandler<EventArgs>? OnGameOver;

    public void SetCell(int index)
    {
        if (State.Board[index] != null)
        {
            return;
        }

        State.Board[index] = State.CurrentPlayer;
        if (CheckWin())
        {
            OnGameWin?.Invoke(this, State.CurrentPlayer);
            return;
        }

        if (CheckGameOver())
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
            return;
        }

        State.CurrentPlayer = State.CurrentPlayer switch
        {
            Player.One => Player.Two,
            Player.Two => Player.One
        };
    }

    public void Reset() => State = new GameState();

    private static readonly int[][] WinConditions =
    [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],

        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],

        [0, 4, 8],
        [2, 4, 6]
    ];

    internal bool CheckWin() => WinConditions.Any(w => new[] { State.Board[w[0]], State.Board[w[1]], State.Board[w[2]] }.All(p => p == State.CurrentPlayer));

    internal bool CheckGameOver() => State.Board.All(c => c != null);
}