using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallCollection
{
    private BallCollisionHandler _ballCollisionDetector;
    
    private readonly List<Ball> _activeBall = new List<Ball>();
    private readonly List<Ball> _ballsTopRow = new List<Ball>();
    private readonly List<Ball> _ballsThrow = new List<Ball>();

    public event Action OnBallsFell;

    public BallCollection(BallCollisionHandler ballCollisionHandler)
    {
        _ballCollisionDetector = ballCollisionHandler;
        _ballCollisionDetector.OnBallMayHaveTakenPlaceTopRowBall += TryReplaceBallTopRow;
    }

    public int CurrentCountBallsThrow => _ballsThrow.Count;
    public int CurrentCountTopRowBalls => _ballsTopRow.Count;
    public int StartCountTopRowBalls { get; private set; }

    public event Action OnBurstBallTopRow;

    public void AddMatrixBall(Ball ball, bool isBallTopRow = false)
    {
        if (isBallTopRow)
        {
            ball.OnBurst += RemoveBallTopRow;
            _ballsTopRow.Add(ball);
        }
        _activeBall.Add(ball);
        StartCountTopRowBalls++;
    }

    public void AddThrowBalls(List<Ball> balls)
    {
        foreach (var ball in balls)
        {
            _ballsThrow.Add(ball);
            _activeBall.Add(ball);
            ball.OnBurst += RemoveBall;
        }
    }

    public void TryReplaceBallTopRow(Ball possibleTopRowBall, Ball newBall)
    {
        Ball ball = _ballsTopRow.Find(x => possibleTopRowBall);

        if (ball)
        {
            newBall.OnBurst += RemoveBallTopRow;
            _ballsTopRow.Add(newBall);
        }
    }

    private void RemoveBall(Ball ball)
    {
        _activeBall.Remove(ball);
    }

    private void RemoveBallTopRow(Ball ball)
    {
        ball.OnBurst -= RemoveBallTopRow;
        _activeBall.Remove(ball);
        _ballsTopRow.Remove(ball);

        OnBurstBallTopRow?.Invoke();
    }

    public void FellBalls()
    {
        foreach (var activeBall in _activeBall)
        {
            activeBall.Fell();
        }
    }

    public Ball GetBallThrowBall()
    {
        Ball firstBall = _ballsThrow[0];
        _ballsThrow.Remove(firstBall);
        return firstBall;
    }
}