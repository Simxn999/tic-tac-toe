using System.Text;

namespace TicTacToe.Navigation;

public class HomePage : Page
{
    public HomePage()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Title = "Tic-Tac-Toe";
    }

    protected override void UpdateOptions()
    {
        Options.Clear();

        Options.Add("#enter#Play", NavigationFactory.Game);
    }
}