using UnityEngine;

public class PlayerWin
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
        _gameBoard.BallCollection.OnBurstBallTopRow -= Win;
    }

    private void CheckNumberRemainingBalls()
    {
        BallCollection ballCollection = _gameBoard.BallCollection;
        
        float percentageBurstTopRowBalls =
            (float)ballCollection.CurrentCountTopRowBalls / (float)ballCollection.StartCountTopRowBalls;
        
        Debug.Log(percentageBurstTopRowBalls);

        if(percentageBurstTopRowBalls <= PERCENTAGE_BALLS_REAMINING_WIN)
            Win();
    }
}