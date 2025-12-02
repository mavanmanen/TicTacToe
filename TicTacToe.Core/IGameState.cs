namespace TicTacToe.Core;

public interface IGameState
{
    public string CurrentPlayer { get; set; }
    public string?[][] Board { get; set; }

    internal void Reset()
    {
        CurrentPlayer = Constants.PlayerOne;
        Board = [
            [null, null, null],
            [null, null, null],
            [null, null, null]
        ];
    }
}