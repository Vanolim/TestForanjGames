using System;
using UnityEngine;

public class BallCollisionHandler
{
    private Ball _ball;

    public event Action OnBallSameTypeCollided;
    public event Action OnBallBurstAnotherBall;
    public event Action<Ball, Ball> OnBallMayHaveTakenPlaceTopRowBall;
    
    public bool IsBallNotSet => !_ball;

    public void SetBall(Ball ball)
    {
        if (_ball != null)
            ResetBall();
        
        _ball = ball;
        _ball.OnCollided += HandleCollision;
        _ball.BallMovement.OnReachedEndPoint += RemoveBall;
    }

    private void HandleCollision(Ball collisionBall)
    {
        if(_ball == null)
            return;

        if (_ball.IsHasBreakThroughPotential)
        {
            _ball.SetBreakthroughPotential(false);
            _ball.BallMovement.StopMoving();
            _ball.BallMovement.MoveToPoint(collisionBall.transform.position);
            OnBallMayHaveTakenPlaceTopRowBall?.Invoke(collisionBall, _ball);
            collisionBall.Burst();
            _ball.SetMatrixActivationAfterArrivingPoint();
            ResetBall();
            OnBallBurstAnotherBall?.Invoke();
        }
        else
        {
            _ball.BallMovement.StopMoving();
            if (CheckCollidingObjectSameType(_ball, collisionBall))
            {
                OnBallSameTypeCollided?.Invoke();
                collisionBall.Burst();
                RemoveBall();
            }
            else
            {
                _ball.SetMatrixActivate();
                ResetBall();
            }
        }
    }

    private void RemoveBall()
    {
        ResetBall(true);
    }

    private void ResetBall(bool burstBall = false)
    {
        _ball.BallMovement.OnReachedEndPoint -= RemoveBall;
        _ball.OnCollided -= HandleCollision;
        if(burstBall)
            _ball.Burst();
        _ball = null;
    }

    private bool CheckCollidingObjectSameType(Ball ball1, Ball ball2) => 
        ball1.BallType == ball2.BallType;
}