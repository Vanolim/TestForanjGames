using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BallSameTypeDetectorHandler : IDisposable
{
    private readonly BallCollisionHandler _ballCollisionHandler;
    private List<Ball> _foundBallsSameType;
    private int _counterWaitingNextCollision;

    private const int MINIMEM_NUMBER_FOUND_BALLS_SAME_TYPE = 2;
    private const int COLLISION_WAIT_COUNTER_START_TIME = 200;
    public int CountFindBallsSamyType => _foundBallsSameType.Count;

    public event Action OnBallsSameTypeFound;
    
    public BallSameTypeDetectorHandler(BallCollisionHandler ballCollisionHandler)
    {
        _ballCollisionHandler = ballCollisionHandler;
        _ballCollisionHandler.OnBallBurstAnotherBall += StartSearching;
    }

    private void StartSearching(Ball ball)
    {
        _foundBallsSameType = new List<Ball>();
        _counterWaitingNextCollision = COLLISION_WAIT_COUNTER_START_TIME;
        WaitingForNextCollision();
        TryFindSameTypeBalls(ball);
    }

    private void TryFindSameTypeBalls(Ball ball)
    {
        ball.BallSameTypeDetector.OnFound += AddBall;
        ball.BallSameTypeDetector.CheckCollidersAround();
    }

    private void AddBall(Ball ball)
    {
        if (_foundBallsSameType.Contains(ball) == false)
        {
            _foundBallsSameType.Add(ball);
            TryFindSameTypeBalls(ball);
            _counterWaitingNextCollision *= _foundBallsSameType.Count;
        }
    }

    private async Task WaitingForNextCollision()
    {
        await Task.Delay(_counterWaitingNextCollision);
        CheckFoundBallsSameType();
    }

    private void CheckFoundBallsSameType()
    {
        int countBallsSameType = _foundBallsSameType.Count;
        
        if (countBallsSameType > MINIMEM_NUMBER_FOUND_BALLS_SAME_TYPE)
        {
            OnBallsSameTypeFound?.Invoke();
            BurstBalls();
        }
    }

    private void BurstBalls()
    {
        foreach (var ball in _foundBallsSameType)
        {
            ball.Burst();
        }
    }

    public void Dispose() => _ballCollisionHandler.OnBallBurstAnotherBall -= StartSearching;
}