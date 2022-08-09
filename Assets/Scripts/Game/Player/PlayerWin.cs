using UnityEngine;

public class PlayerWin : IDisposable
{
    private readonly GameBoard _gameBoard;
    private readonly GameSceneUI _gameSceneUI;

    private const float PERCENTAGE_BALLS_REAMINING_WIN = 0.3f;
    
    public PlayerWin(GameBoard gameBoard, GameSceneUI gameSceneUI)
    {
        _gameBoard = gameBoard;
        _gameSceneUI = gameSceneUI;
        _gameBoard.BallCollection.OnBurstBallTopRow += CheckNumberRemainingBalls;
    }

    private void Win()
    {
        _gameBoard.AllBallsFeel();
        _gameSceneUI.ActivateWinView();
    }

    private void CheckNumberRemainingBalls()
    {
        if(GetCurrentPercentageBurstBallsTopRow() <= PERCENTAGE_BALLS_REAMINING_WIN)
            Win();
    }
    
    private float GetCurrentPercentageBurstBallsTopRow() =>
        (float)_gameBoard.BallCollection.CurrentCountTopRowBalls / (float)_gameBoard.BallCollection.StartCountTopRowBalls;

    public void Dispose()
    {
        _gameBoard.BallCollection.OnBurstBallTopRow -= CheckNumberRemainingBalls;
    }
}