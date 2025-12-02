namespace TicTacToe.Core.Test;

public class EngineUnitTests
{
    [Fact]
    public void Engine_RowWin_TriggersWinEvent()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        state.Board =
        [
            ["X", "X", null],
            [null, null, null],
            [null, null, null]
        ];

        // Act / Assert
        Assert.Raises<string>(
            h => sut.OnGameWin += h,
            h => sut.OnGameWin -= h,
            () => sut.SetCell(0, 2));
    }

    [Fact]
    public void Engine_ColumnWin_TriggersWinEvent()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        state.Board =
        [
            ["X", null, null],
            ["X", null, null],
            [null, null, null]
        ];

        // Act / Assert
        Assert.Raises<string>(
            h => sut.OnGameWin += h,
            h => sut.OnGameWin -= h,
            () => sut.SetCell(2, 0));
    }

    [Fact]
    public void Engine_DiagonalLeftWin_TriggersWinEvent()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        state.Board =
        [
            ["X", null, null],
            [null, "X", null],
            [null, null, null]
        ];

        // Act / Assert
        Assert.Raises<string>(
            h => sut.OnGameWin += h,
            h => sut.OnGameWin -= h,
            () => sut.SetCell(2, 2));
    }

    [Fact]
    public void Engine_DiagonalRightWin_TriggersWinEvent()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        state.Board =
        [
            [null, null, "X"],
            [null, "X", null],
            [null, null, null]
        ];

        // Act / Assert
        Assert.Raises<string>(
            h => sut.OnGameWin += h,
            h => sut.OnGameWin -= h,
            () => sut.SetCell(2, 0));
    }

    [Fact]
    public void Engine_GameOver_TriggersGameOverEvent()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        state.Board =
        [
            ["O", "O", "X"],
            ["X", "X", "O"],
            ["O", "O", null]
        ];

        // Act / Assert
        Assert.Raises<EventArgs>(
            h => sut.OnGameOver += h,
            h => sut.OnGameOver -= h,
            () => sut.SetCell(2, 2));
    }

    [Fact]
    public void Engine_Reset_ResetsBoard()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        state.Board =
        [
            ["O", "O", "X"],
            ["X", "X", "O"],
            ["O", "O", "O"]
        ];

        string?[][] expectedBoardValue =
        [
            [null, null, null],
            [null, null, null],
            [null, null, null]
        ];

        // Act
        sut.Reset();

        // Assert
        Assert.Equal(expectedBoardValue, state.Board);
    }

    [Fact]
    public void Engine_SetCell_ChangesToNextPlayer()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        
        // Act 1
        sut.SetCell(0, 0);
        
        // Assert 1
        Assert.Equal(Constants.PlayerTwo, state.CurrentPlayer);
        
        // Act 2
        sut.SetCell(0, 1);
        
        // Assert 2
        Assert.Equal(Constants.PlayerOne, state.CurrentPlayer);
    }
    
    [Fact]
    public void Engine_SetCell_CannotChangeAlreadySetCell()
    {
        // Arrange
        var state = new TestState();
        var sut = new Engine(state);
        
        // Act
        sut.SetCell(0, 0);
        
        // Assert 1
        Assert.Equal(Constants.PlayerTwo, state.CurrentPlayer);
        
        // Act 2
        sut.SetCell(0, 0);
        
        // Assert 2
        Assert.Equal(Constants.PlayerTwo, state.CurrentPlayer);
    }
}