using System.Text;
using TicTacToe.Interfaces;
using TicTacToe.Navigation;
using TicTacToe.Observer;

namespace TicTacToe;

// Observer Pattern
public class TicTacToe : Broadcaster
{
    public Board Board => GameFactory.GameBoard;
    public Entity CurrentPlayer { get; private set; }
    public Entity? Result { get; private set; }

    public void Start()
    {
        CurrentPlayer = Entity.X;
        Board.Reset();
        SetResult();
    }

    public void PlayTurn(int squareID)
    {
        Board.SetSquare(squareID, CurrentPlayer);
        
        ToggleCurrentPlayer();
        CheckForWinner();
    }

    void ToggleCurrentPlayer()
    {
        CurrentPlayer = CurrentPlayer switch
                        {
                            Entity.X => Entity.O,
                            Entity.O => Entity.X,
                            _ => Entity.X
                        };
    }

    void SetResult(Entity? result = null)
    {
        Result = result;

        // Observer Pattern
        if (Result is not null) NotifyResult();
    }
    
    void CheckForWinner()
    {
        var winningCombos = new List<IList<ISquare>>
        {
            Board.Where(s => s.Coordinate.X == s.Coordinate.Y).ToList(),
            Board.Where(s => s.Coordinate.Y == Board.Size - s.Coordinate.X + 1).ToList()
        };

        for (var i = 1; i <= Board.Size; i++)
        {
            winningCombos.Add(Board.Where(s => s.Coordinate.X == i).ToList());
            winningCombos.Add(Board.Where(s => s.Coordinate.Y == i).ToList());
        }

        if (winningCombos.Any(c => c.All(s => s.Entity == Entity.X)))
        {
            SetResult(Entity.X);
            return;
        }

        if (winningCombos.Any(c => c.All(s => s.Entity == Entity.O)))
        {
            SetResult(Entity.O);
            return;
        }

        if (Board.All(s => s.Entity != Entity.Empty))
        {
            SetResult(Entity.Empty);
            return;
        }
        
        SetResult();
    }
}