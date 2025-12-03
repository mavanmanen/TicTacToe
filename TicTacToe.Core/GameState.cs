namespace TicTacToe.Core;

public class GameState
{
    public Player CurrentPlayer { get; set; } = Player.One;
    public Player?[] Board { get; internal set; } = [null, null, null, null, null, null, null, null, null];
}