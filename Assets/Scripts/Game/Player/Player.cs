public class Player : IDisposable
{
    private PlayerLose _playerLose;
    private PlayerWin _playerWin;

    public Player(GameBoard gameBoard, GameSceneUI gameSceneUI)
    {
        _playerWin = new PlayerWin(gameBoard, gameSceneUI);
        _playerLose = new PlayerLose(gameBoard, gameSceneUI);
    }

    public void Dispose()
    {
        _playerLose.Dispose();
        _playerWin.Dispose();
    }
}