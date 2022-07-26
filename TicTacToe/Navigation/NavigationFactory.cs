namespace TicTacToe.Navigation;

// Factory Pattern & Singleton Pattern
public static class NavigationFactory
{
    public static HomePage HomePage => _lazyHomePage.Value;
    public static GamePage GamePage => _lazyGamePage.Value;
    public static ResultPage ResultPage => _lazyResultPage.Value;
    
    static readonly Lazy<HomePage> _lazyHomePage = new Lazy<HomePage>(() => new HomePage());
    static readonly Lazy<GamePage> _lazyGamePage = new Lazy<GamePage>(() => new GamePage());
    static readonly Lazy<ResultPage> _lazyResultPage = new Lazy<ResultPage>(() => new ResultPage());

    public static void Home()
    {
        HomePage.Run();
    }

    public static void Game()
    {
        GameFactory.Launch();
        GamePage.Run();
    }

    public static void Result()
    {
        GamePage.Exit = true;
        ResultPage.Run();
    }
}