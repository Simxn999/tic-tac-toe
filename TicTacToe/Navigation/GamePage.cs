using TicTacToe.Observer;

namespace TicTacToe.Navigation;

public class GamePage : Page
{
    TicTacToe _engine = GameFactory.Engine;
    
    public GamePage()
    {
        Content = GameContent;
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        foreach (var square in _engine.Board)
        {
            if (square.Entity == Entity.Empty)
            {
                Options.Add($"Square {square.SquareID}", () => { _engine.PlayTurn(square.SquareID); });
                continue;
            }
            
            Options.Add($"!disabled!Square {square.SquareID}", () => { });
        }
    }
    
    void GameContent()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"Current Player: {_engine.CurrentPlayer}");

        DrawEmptyBoard();
        DrawBoardData();

        Console.SetCursorPosition(0, Board.Size * 2 + 2);
    }

    void DrawEmptyBoard()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(" ┌───┬───┬───┐ ");
        Console.WriteLine(" │   │   │   │ ");
        Console.WriteLine(" ├───┼───┼───┤ ");
        Console.WriteLine(" │   │   │   │ ");
        Console.WriteLine(" ├───┼───┼───┤ ");
        Console.WriteLine(" │   │   │   │ ");
        Console.WriteLine(" └───┴───┴───┘ ");
        Console.ResetColor();
    }

    void DrawBoardData()
    {
        foreach (var square in _engine.Board)
        {
            Console.SetCursorPosition(square.Coordinate.X * 4 - 1, square.Coordinate.Y * 2);
            if (square.Entity != Entity.Empty)
            {
                Console.Write(square.Entity);
                continue;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(square.SquareID);
            Console.ResetColor();
        }
    }
}