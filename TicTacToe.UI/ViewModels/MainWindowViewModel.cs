using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicTacToe.Core;

namespace TicTacToe.UI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IEngine _engine;
    public IGameState GameState { get; }

    public MainWindowViewModel(IEngine engine, IGameState gameState)
    {
        _engine = engine;
        _engine.OnGameWin += (_, winner) => ShowGameEndMessageBox("Game Won!", $"Game won by {winner}");
        _engine.OnGameOver += (_, _) => ShowGameEndMessageBox("Game Over!", "No Winner.");

        GameState = gameState;
    }
    
    private void ShowGameEndMessageBox(string title, string message)
    {
        OnPropertyChanged(nameof(GameState));
        if (MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK) == MessageBoxResult.OK)
        {
            _engine.Reset();
            OnPropertyChanged(nameof(GameState));
        }
    }

    [RelayCommand]
    private void CellClick(string location)
    {
        var splitLocation = location.Split(',');
        var x = int.Parse(splitLocation[0]);
        var y = int.Parse(splitLocation[1]);

        _engine.SetCell(x, y);
        OnPropertyChanged(nameof(GameState));
    }
}