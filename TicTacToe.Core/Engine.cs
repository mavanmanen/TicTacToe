namespace TicTacToe.Core;

public sealed class Engine : IEngine
{
    private readonly IGameState _state;

    public Engine(IGameState state)
    {
        _state = state;
        _state.Reset();
    }

    public event EventHandler<string>? OnGameWin;
    public event EventHandler<EventArgs>? OnGameOver;

    public void SetCell(int x, int y)
    {
        if (_state.Board[x][y] != null)
        {
            return;
        }
        
        _state.Board[x][y] = _state.CurrentPlayer;
        if (CheckWin())
        {
            var currentPlayerName = _state.CurrentPlayer ==  Constants.PlayerOne ? "Player One" : "Player Two";
            OnGameWin?.Invoke(this, currentPlayerName);
            return;
        }

        if (CheckGameOver())
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
            return;
        }
        
        _state.CurrentPlayer = _state.CurrentPlayer == Constants.PlayerOne ? Constants.PlayerTwo : Constants.PlayerOne;
    }

    public void Reset() => _state.Reset();
    
    private bool CheckWin()
    {
        for (var x = 0; x < 3; x++)
        {
            // Rows
            if (_state.Board[x].Count(cell => cell == _state.CurrentPlayer) == 3)
            {
                return true;
            }

            // Columns
            if (_state.Board[0][x] == _state.CurrentPlayer && _state.Board[1][x] == _state.CurrentPlayer && _state.Board[2][x] == _state.CurrentPlayer)
            {
                return true;
            }
        }

        // Left Diagonal
        if (_state.Board[0][0] == _state.CurrentPlayer && _state.Board[1][1] == _state.CurrentPlayer && _state.Board[2][2] == _state.CurrentPlayer)
        {
            return true;
        }

        // Right Diagonal
        if (_state.Board[0][2] == _state.CurrentPlayer && _state.Board[1][1] == _state.CurrentPlayer && _state.Board[2][0] == _state.CurrentPlayer)
        {
            return true;
        }
        
        
        return false;
    }

    private bool CheckGameOver() => _state.Board.SelectMany(cell => cell).All(cell => cell != null);
}