namespace TicTacToe.Core.Test;

public class EngineUnitTests
{
    public static readonly TheoryData<int[]> WinConditions =
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

    private static readonly Player?[] GameOverBoard =
    [
        Player.One, Player.Two, Player.One,
        Player.One, Player.One, Player.Two,
        Player.Two, Player.One, Player.Two
    ];

    [Theory]
    [MemberData(nameof(WinConditions))]
    public void Engine_CheckWin_ReturnsTrueOnWin(int[] winningIndexes)
    {
        // Arrange
        var sut = new Engine();
        foreach (var index in winningIndexes)
        {
            sut.State.Board[index] = Player.One;
        }

        // Act
        var result = sut.CheckWin();

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(WinConditions))]
    public void Engine_CheckWin_ReturnsFalseOnNoWin(int[] winningIndexes)
    {
        // Arrange
        var sut = new Engine();
        foreach (var index in winningIndexes)
        {
            sut.State.Board[index] = Player.One;
        }

        sut.State.Board[winningIndexes[2]] = Player.Two;

        // Act
        var result = sut.CheckWin();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Engine_CheckGameOver_ReturnsTrueOnGameOver()
    {
        // Arrange
        var sut = new Engine
        {
            State =
            {
                Board = GameOverBoard
            }
        };

        // Act
        var result = sut.CheckGameOver();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Engine_CheckGameOver_ReturnsFalseOnNoGameOver()
    {
        // Arrange
        var sut = new Engine
        {
            State =
            {
                Board =
                [
                    Player.One, Player.Two, Player.One,
                    Player.One, Player.One, Player.Two,
                    Player.Two, Player.One, null
                ]
            }
        };

        // Act
        var result = sut.CheckGameOver();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void Engine_SetCell_SetsCellToCurrentPlayer(int index)
    {
        // Arrange
        var sut = new Engine();

        // Act
        sut.SetCell(index);

        // Assert
        Assert.Equal(Player.One, sut.State.Board[index]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void Engine_SetCell_DoesntSetCellIfAlreadySet(int index)
    {
        // Arrange
        var sut = new Engine();

        // Act
        sut.SetCell(index);
        sut.SetCell(index);

        // Assert
        Assert.NotEqual(Player.Two, sut.State.Board[index]);
    }

    [Theory]
    [MemberData(nameof(WinConditions))]
    public void Engine_SetCell_TriggersWinEvent(int[] winningIndexes)
    {
        // Arrange
        var sut = new Engine();
        sut.State.Board[winningIndexes[0]] = Player.One;
        sut.State.Board[winningIndexes[1]] = Player.One;

        // Act
        var raisedEvent = Assert.Raises<Player>(
            a => sut.OnGameWin += a,
            d => sut.OnGameWin -= d,
            () => sut.SetCell(winningIndexes[2]));

        // Assert
        Assert.NotNull(raisedEvent);
        Assert.Equal(Player.One, raisedEvent.Arguments);
    }

    [Fact]
    public void Engine_SetCell_TriggersGameOverEvent()
    {
        // Arrange
        var sut = new Engine
        {
            State =
            {
                Board = GameOverBoard
            }
        };
        sut.State.Board[0] = null;
        
        // Act
        var raisedEvent = Assert.Raises<EventArgs>(
            a => sut.OnGameOver += a,
            d => sut.OnGameOver -= d,
            () => sut.SetCell(0));

        // Assert
        Assert.NotNull(raisedEvent);
    }

    [Theory]
    [InlineData(Player.One, Player.Two)]
    [InlineData(Player.Two, Player.One)]
    public void Engine_SetCell_ChangesPlayer(Player startingPlayer, Player expectedPlayer)
    {
        // Arrange
        var sut = new Engine
        {
            State =
            {
                CurrentPlayer = startingPlayer
            }
        };
        
        // Act
        sut.SetCell(0);
        
        // Assert
        Assert.Equal(expectedPlayer, sut.State.CurrentPlayer);
    }

    [Fact]
    public void Engine_Reset_ResetsState()
    {
        // Arrange
        Player?[] emptyBoard = [null, null, null, null, null, null, null, null, null];
        var sut = new Engine()
        {
            State =
            {
                CurrentPlayer = Player.Two,
                Board = GameOverBoard
            }
        };
        
        // Act
        sut.Reset();
        
        // Assert
        Assert.Equal(Player.One, sut.State.CurrentPlayer);
        Assert.Equal(emptyBoard, sut.State.Board);
    }
}