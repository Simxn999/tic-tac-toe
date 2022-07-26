using TicTacToe.Interfaces;

namespace TicTacToe;

public class Square : ISquare
{
    public int SquareID { get; init; }
    public ICoordinate Coordinate { get; init; }
    public Entity Entity { get; set; } = Entity.Empty;

    public Square(int id, ICoordinate coordinate)
    {
        SquareID = id;
        Coordinate = coordinate;
    }
}