using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicTacToe.Core;

namespace TicTacToe.UI.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IEngine _engine;

    public GameState GameState => _engine.State;

    public MainWindowViewModel(IEngine engine)
    {
        _engine = engine;
        _engine.OnGameWin += (_, winner) => ShowGameEndMessageBox("Game Won!", $"Game won by Player {winner:G}");
        _engine.OnGameOver += (_, _) => ShowGameEndMessageBox("Game Over!", "No Winner.");
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
    private void CellClick(string indexString)
    {
        var index = int.Parse(indexString);
        _engine.SetCell(index);
        OnPropertyChanged(nameof(GameState));
    }
}