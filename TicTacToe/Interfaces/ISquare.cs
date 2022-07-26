namespace TicTacToe.Interfaces;

public interface ISquare
{
    int SquareID { get; init; }
    ICoordinate Coordinate { get; init; }
    Entity Entity { get; set; }
}