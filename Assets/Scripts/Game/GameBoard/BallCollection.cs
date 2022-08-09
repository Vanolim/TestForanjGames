using System;
using System.Collections.Generic;
using UnityEngine;

public class BallCollection : IDisposable
{
    private readonly BallCollisionHandler _ballCollisionDetector;
    
    private readonly List<Ball> _activeBall = new List<Ball>();
    private readonly List<Ball> _ballsTopRow = new List<Ball>();
    private readonly List<Ball> _ballsThrow = new List<Ball>();

    public BallCollection(BallCollisionHandler ballCollisionHandler)
    {
        _ballCollisionDetector = ballCollisionHandler;
        _ballCollisionDetector.OnBallMayHaveTakenPlaceTopRowBall += TryReplaceBallTopRow;
    }

    public int CurrentCountBallsThrow => _ballsThrow.Count;
    public int CurrentCountTopRowBalls => _ballsTopRow.Count;
    public int StartCountTopRowBalls { get; private set; }

    public event Action OnBurstBallTopRow;

    public void SetThrowBalls(List<Ball> balls)
    {
        foreach (var ball in balls)
        {
            _ballsThrow.Add(ball);
        }
    }

    public void AddMatrixBall(Ball ball, bool isBallTopRow = false)
    {
        if (isBallTopRow)
        {
            AddBallTopRaw(ball);
            StartCountTopRowBalls++;
        }
        else
        {
            ball.OnBurst += RemoveBall;
        }

        _activeBall.Add(ball);
    }

    private void TryReplaceBallTopRow(Ball possibleTopRowBall, Ball newBall)
    {
        if (_ballsTopRow.Contains(possibleTopRowBall)) 
            AddBallTopRaw(newBall);
    }

    private void AddBallTopRaw(Ball ball)
    {
        ball.OnBurst += RemoveBallTopRow;
        _ballsTopRow.Add(ball);
    }

    public Ball GetThrowBall()
    {
        Ball firstBall = _ballsThrow[0];
        _ballsThrow.Remove(firstBall);
        _activeBall.Add(firstBall);
        firstBall.OnBurst += RemoveBall;
        return firstBall;
    }
    
    private void RemoveBall(Ball ball)
    {
        ball.OnBurst -= RemoveBall;
        _activeBall.Remove(ball);
    }

    private void RemoveBallTopRow(Ball ball)
    {
        ball.OnBurst -= RemoveBallTopRow;
        _ballsTopRow.Remove(ball);
        _activeBall.Remove(ball);
        OnBurstBallTopRow?.Invoke();
    }

    public void FellBalls()
    {
        foreach (var activeBall in _activeBall)
        {
            activeBall.Fell();
        }
    }

    public void Dispose() => 
        _ballCollisionDetector.OnBallMayHaveTakenPlaceTopRowBall -= TryReplaceBallTopRow;
}