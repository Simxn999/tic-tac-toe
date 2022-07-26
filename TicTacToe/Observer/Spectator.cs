using TicTacToe.Navigation;

namespace TicTacToe.Observer;

// Observer Pattern
public class Spectator
{
    public static void Update()
    {
        NavigationFactory.Result();
    }
}