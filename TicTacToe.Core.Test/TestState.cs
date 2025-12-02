namespace TicTacToe.Core.Test;

internal class TestState : IGameState
{
    public string CurrentPlayer { get; set; }
    public string?[][] Board { get; set; }
}