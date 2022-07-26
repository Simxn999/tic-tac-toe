namespace TicTacToe.Observer;

// Observer Pattern
public class Broadcaster
{
    IList<Spectator> _spectators;
    
    public Broadcaster()
    {
        _spectators = new List<Spectator>();
    }

    public void NotifyResult()
    {
        foreach (var spectator in _spectators)
        {
            Spectator.Update();
        }
    }
    
    public void Subscribe(Spectator spectator)
    {
        if (_spectators.Contains(spectator)) return;
        
        _spectators.Add(spectator);
    }
    
    public void Unsubscribe(Spectator spectator)
    {
        if (!_spectators.Contains(spectator)) return;
        
        _spectators.Remove(spectator);
    }
}