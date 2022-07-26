using TicTacToe.Navigation;
using TicTacToe.Observer;

namespace TicTacToe;

// Factory Pattern & Singleton Pattern
public static class GameFactory
{
    public static TicTacToe Engine => _lazyEngine.Value;
    public static Board GameBoard => _lazyBoard.Value;
    public static Spectator ResultAnnouncer => _lazySpectator.Value;

    static readonly Lazy<TicTacToe> _lazyEngine = new Lazy<TicTacToe>(() => new TicTacToe());
    static readonly Lazy<Board> _lazyBoard = new Lazy<Board>(() => new Board());
    static readonly Lazy<Spectator> _lazySpectator = new Lazy<Spectator>(() => new Spectator());

    public static void Launch()
    {
        // Observer Pattern
        Engine.Subscribe(ResultAnnouncer);
        
        Engine.Start();
    }
}