using System;

public class BallCollisionHandler
{
    private Ball _ball;
    public event Action<Ball> OnBallBurstAnotherBall;
    public event Action<Ball, Ball> OnBallMayHaveTakenPlaceTopRowBall;
    
    public bool IsBallNotSet => !_ball;

    public void SetBall(Ball ball)
    {
        if (_ball != null)
            ResetBall();
        
        _ball = ball;
        _ball.OnCollided += HandleCollision;
        _ball.BallMovement.OnReachedEndPoint += BurstCurrentBall;
    }

    private void HandleCollision(Ball collisionBall)
    {
        if(_ball == null)
            return;

        if (_ball.IsHasBreakThroughPotential)
        {
            PutBallPlaceAnotherBall(collisionBall);
            
            collisionBall.Burst();
            ResetBall();
        }
        else
        {
            StopBall();
            ResetBall();
        }
    }

    private void PutBallPlaceAnotherBall(Ball collisionBall)
    {
        _ball.SetBreakthroughPotential(false);
        _ball.BallMovement.MoveToPoint(collisionBall.transform.position);
        OnBallMayHaveTakenPlaceTopRowBall?.Invoke(collisionBall, _ball);
        _ball.SetMatrixActivationAfterArrivingPoint();
        OnBallBurstAnotherBall?.Invoke(_ball);
    }

    private void StopBall()
    {
        _ball.BallMovement.StopMoving();
        _ball.SetMatrixActivate();
    }

    private void BurstCurrentBall() => ResetBall(true);

    private void ResetBall(bool burstBall = false)
    {
        _ball.BallMovement.OnReachedEndPoint -= BurstCurrentBall;
        _ball.OnCollided -= HandleCollision;
        
        if(burstBall)
            _ball.Burst();
        _ball = null;
    }
}