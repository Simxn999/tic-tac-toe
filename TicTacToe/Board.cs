using TicTacToe.Interfaces;

namespace TicTacToe;

public class Board : List<ISquare>
{
    public const int Size = 3;

    public Board() : base(Size * Size)
    {
        for (var y = 1; y <= Size; y++)
        {
            for (var x = 1; x <= Size; x++)
            {
                Add(new Square(Count + 1, new Coordinate(x, y)));
            }
        }
    }

    public ISquare? GetSquare(int squareID)
    {
        return this.FirstOrDefault(s => s.SquareID == squareID);
    }
    
    public void SetSquare(int squareID, Entity value)
    {
        var square = GetSquare(squareID);
        
        if (square is null) return;

        square.Entity = value;
    }
    
    public void Reset()
    {
        foreach (var square in this)
        {
            square.Entity = Entity.Empty;
        }
    }
}