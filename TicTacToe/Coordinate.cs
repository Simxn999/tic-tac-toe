using TicTacToe.Interfaces;

namespace TicTacToe;

public class Coordinate : ICoordinate
{
    public int X { get; init; }  
    public int Y { get; init; }  

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}