using CommunityToolkit.Mvvm.ComponentModel;
using TicTacToe.Core;

namespace TicTacToe.UI.ViewModels;

public sealed class GameStateViewModel : ObservableObject, IGameState
{
    private string _currentPlayer;

    public string CurrentPlayer
    {
        get => _currentPlayer;
        set
        {
            if (value == _currentPlayer) return;
            _currentPlayer = value;
            OnPropertyChanged();
        }
    }

    private string?[][] _board;
    
    public string?[][] Board
    {
        get => _board;
        set
        {
            if (Equals(value, _board)) return;
            _board = value;
            OnPropertyChanged();
        }
    }
}