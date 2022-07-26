namespace TicTacToe.Navigation;

public class ResultPage : Page
{
    static Entity? _result => GameFactory.Engine.Result;
    
    public ResultPage()
    {
        Content = ResultContent;
    }
    
    protected override void UpdateOptions()
    {
        Options.Clear();
        
        Options.Add("#enter#Play Again",
                    () =>
                    {
                        NavigationFactory.Game();
                        Exit = true;
                    });
    }

    void ResultContent()
    {
        Console.SetCursorPosition(0, 0);
        
        switch (_result)
        {
            case Entity.X or Entity.O:
                Console.WriteLine($"Player {_result} win!");
                break;
            
            case Entity.Empty: 
                Console.WriteLine("The game ended in a draw!");
                break;
            
            default: 
                break;
        }
    }
}