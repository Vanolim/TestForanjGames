using System.Threading.Tasks;
using UnityEngine;

public class PlayerLose : IDisposable
{
    private readonly GameBoard _gameBoard;
    private readonly GameSceneUI _gameSceneUI;

    private const int EXPECTATION_LOSE = 5000;
    public PlayerLose(GameBoard gameBoard, GameSceneUI gameSceneUI)
    {
        _gameSceneUI = gameSceneUI;
        _gameBoard = gameBoard;
        _gameBoard.OnBallsThrowOut += StarterLose;
    }

    private void Lose()
    {
        _gameBoard.AllBallsFeel();
        _gameSceneUI.ActivateLoseView();
    }

    private void StarterLose() => ExpectationLoss(EXPECTATION_LOSE);

    private async Task ExpectationLoss(int time)
    {
        await Task.Delay(time);
        Lose();
    }

    public void Dispose() => _gameBoard.OnBallsThrowOut -= StarterLose;
}