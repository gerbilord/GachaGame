public class PlayerActionResult
{
    PlayerAction _playerAction;
    PlayerBoard _player1BoardSnapshot;
    PlayerBoard _player2BoardSnapshot;
    
    public PlayerActionResult(PlayerAction playerAction, PlayerBoard player1BoardSnapshot, PlayerBoard player2BoardSnapshot)
    {
        _playerAction = playerAction;
        _player1BoardSnapshot = player1BoardSnapshot;
        _player2BoardSnapshot = player2BoardSnapshot;
    }

    public PlayerBoard GetPlayer1BoardSnapshot()
    {
        return _player1BoardSnapshot;
    }

    public PlayerBoard GetPlayer2BoardSnapshot()
    {
        return _player2BoardSnapshot;
    }

    public PlayerAction GetPlayerAction()
    {
        return _playerAction;
    }
}